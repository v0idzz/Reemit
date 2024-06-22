using Reemit.Decompiler.Clr.Metadata.Tables;
using Reemit.Decompiler.Clr.Methods;

namespace Reemit.Decompiler;

public class ClrMethod(
    string name,
    int maxStack,
    byte[] methodBody)
{
    public string Name { get; } = name;
    public int MaxStack { get; } = maxStack;
    public byte[] MethodBody { get; } = methodBody;

    public static ClrMethod FromMethodDefRow(MethodDefRow methodDefRow, ModuleReaderContext context)
    {
        var name = context.StringsHeapStream.Read(methodDefRow.Name);
        
        // TODO: Investigate what exactly it means when a MethodDef row has its RVA set to 0
        if (methodDefRow.Rva == 0)
        {
            return new ClrMethod(name, 0, []);
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

        return new ClrMethod(name, maxStack, methodBody);
    }
}