using Reemit.Disassembler.Clr.Metadata.Tables;
using Reemit.Disassembler.Clr.Methods;
using Reemit.Disassembler.Clr.Signatures;

namespace Reemit.Disassembler;

public class ClrMethod(
    string name,
    MethodSig signature,
    int maxStack,
    IReadOnlyList<ClrMethodParam> @params,
    byte[] methodBody,
    ClrTypeInfo retType)
{
    public string Name { get; } = name;
    public MethodSig Signature { get; } = signature;
    public int MaxStack { get; } = maxStack;
    public IReadOnlyList<ClrMethodParam> Params { get; } = @params;
    public ClrTypeInfo RetType { get; } = retType;
    public byte[] MethodBody { get; } = methodBody;

    public int GenericParamCount { get; } =
        (int)(signature is GenericMethodSig genericMethodSig ? genericMethodSig.GenParamCount : 0);

    public static ClrMethod FromMethodDefRow(MethodDefRow methodDefRow, ModuleReaderContext context)
    {
        var name = context.StringsHeapStream.Read(methodDefRow.Name);

        var signatureReader = context.BlobHeapStream.CreateBlobReader(methodDefRow.Signature);
        var signature = MethodSig.Read(signatureReader);
        
        var typeSigMapper = new TypeSigToClrTypeInfoMapper(context);

        var @params =
            context.TableReferenceResolver.GetReferencedRows<MethodDefRow, ParamRow>(methodDefRow, r => r.ParamList);

        // TODO: Investigate what exactly it means when a MethodDef row has its RVA set to 0
        if (methodDefRow.Rva == 0)
        {
            return new ClrMethod(name, signature, 0, @params
                // From ECMA-335 "II.22.33 Param : 0x08":
                // A Sequence value of 0 refers to the owner method’s return type
                .Where(x => x.Sequence != 0)
                .OrderBy(x => x.Sequence)
                .Select((x, i) => new ClrMethodParam(context.StringsHeapStream.Read(x.Name),
                    typeSigMapper.Map(signature.Params[i].Type)))
                .ToArray(), [], typeSigMapper.Map(signature.RetType.Type));
        }

        var corILMethodOffset = context.PEFile.GetFileOffset(methodDefRow.Rva);
        var corILMethodReader = context.PEFile.CreateReaderAt(corILMethodOffset);

        var methodHeader = MethodHeaderReader.ReadMethodHeader(corILMethodReader);

        var methodBody = corILMethodReader.ReadBytes((int)methodHeader.CodeSize);

        if (methodHeader is FatMethodHeader fatMethodHeader)
        {
            if (fatMethodHeader.Flags.HasFlag(CorILMethodFlags.MoreSects))
            {
                MethodDataSectionsReader.ReadMethodDataSections(corILMethodReader).ToArray();
            }
        }

        var maxStack = methodHeader is TinyMethodHeader
            // From ECMA-335 II.25.4:
            // A method is given a tiny header if it has no local variables, maxstack is 8 or less, 
            ? (ushort)8
            : ((FatMethodHeader)methodHeader).MaxStack;

        return new ClrMethod(name, signature, maxStack, @params
            // From ECMA-335 "II.22.33 Param : 0x08":
            // A Sequence value of 0 refers to the owner method’s return type
            .Where(x => x.Sequence != 0)
            .OrderBy(x => x.Sequence)
            .Select((x, i) => new ClrMethodParam(context.StringsHeapStream.Read(x.Name),
                typeSigMapper.Map(signature.Params[i].Type)))
            .ToArray(), methodBody, typeSigMapper.Map(signature.RetType.Type));
    }
}