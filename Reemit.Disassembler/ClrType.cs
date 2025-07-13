using Reemit.Common;
using Reemit.Disassembler.Clr.Metadata;
using Reemit.Disassembler.Clr.Metadata.Tables;

namespace Reemit.Disassembler;

public class ClrType
{
    public bool IsValueType { get; }
    public bool IsInterface { get; }
    public RangeMapped<string> Name { get; }
    public string Namespace { get; }
    public IReadOnlyList<ClrTypeInfo> ImplementsInterfaces { get; }
    public IReadOnlyList<ClrMethod> Methods { get; }

    private ClrType(bool isInterface,
        bool isValueType,
        RangeMapped<string> name,
        string @namespace,
        IReadOnlyList<ClrTypeInfo> implementsInterfaces,
        IReadOnlyList<ClrMethod> methods)
    { 
        IsInterface = isInterface;
        IsValueType = isValueType;
        Name = name;
        Namespace = @namespace;
        ImplementsInterfaces = implementsInterfaces;
        Methods = methods;
    }


    public static ClrType FromTypeDefRow(TypeDefRow typeDefRow, ModuleReaderContext context)
    {
        var stringsHeap = context.StringsHeapStream;
        // TODO: Check if also derives ultimately from System.Object
        var isInterface = typeDefRow.ClassSemantics == TypeClassSemanticsAttributes.Interface;
        var isValueType = false;

        var typeDefOrRefMapper = new TypeDefOrRefToClrTypeInfoMapper(context);
        var interfaceRows = context.MetadataTablesStream.InterfaceImpl?.Rows ?? [];

        var interfaces = interfaceRows
            .Where(i => i.Class == typeDefRow.Rid)
            .Select(i => typeDefOrRefMapper.TryResolveDefOrRefCodedIndex(i.Interface, out var typeInfo) ? typeInfo : null)
            .Where(i => i != null)
            .ToArray();

        /*
         * From ECMA-335 II.22.37:
         * Note that any type shall be one, and only one, of
           - Class (Flags.Interface = 0, and derives ultimately from System.Object)
           - Interface (Flags.Interface = 1)
           - Value type, derived ultimately from System.ValueType
         */
        // HACK: Check if the type extends System.ValueType to tell if it's a value type.
        // Currently, we are not able to properly resolve the Extends CodedIndex as we are still lacking
        // implementation of some tables it can reference. So, for that reason, this hack below was implemented.
        // It only takes the Extends RID into consideration if it references TypeRef table.
        // TODO: This should be replaced with something better as soon as we have the aforementioned tables implemented. 
        if (typeDefRow.Extends.ReferencedTable == MetadataTableName.TypeRef)
        {
            var extendsRow =
                context.MetadataTablesStream.TypeRef?.Rows.ElementAtOrDefault((int)typeDefRow.Extends.ZeroBasedIndex);

            if (extendsRow is not null)
            {
                var extendsTypeName = stringsHeap.Read(extendsRow.TypeName);
                var extendsTypeNamespace = stringsHeap.Read(extendsRow.TypeNamespace);
                isValueType = $"{extendsTypeNamespace}.{extendsTypeName}" == "System.ValueType";
            }
        }

        var type = new ClrType(
            isInterface,
            isValueType,
            stringsHeap.ReadMapped(typeDefRow.TypeName),
            stringsHeap.Read(typeDefRow.TypeNamespace),
            interfaces,
            GetMethods()
        );

        return type;

        IReadOnlyList<ClrMethod> GetMethods()
        {
            var methods = context.TableReferenceResolver.GetReferencedRows<TypeDefRow, MethodDefRow>(typeDefRow, x => x.MethodList);
            return methods.Select(m => ClrMethod.FromMethodDefRow(m, context)).ToArray().AsReadOnly();
        }
    }
}