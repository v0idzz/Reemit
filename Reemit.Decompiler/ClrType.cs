using Reemit.Decompiler.Clr.Metadata;
using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler;

public class ClrType(bool isInterface, bool isValueType, string name, string @namespace)
{
    public bool IsValueType => isValueType;
    public bool IsInterface => isInterface;
    public string Name => name;
    public string Namespace => @namespace;

    public static ClrType FromTypeDefRow(TypeDefRow typeDefRow, ClrMetadataContext context)
    {
        var stringsHeap = context.StringsHeapStream;
        // TODO: Check if also derives ultimately from System.Object
        var isInterface = typeDefRow.ClassSemantics == TypeClassSemanticsAttributes.Interface;
        var isValueType = false;

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

        var type = new ClrType(isInterface, isValueType,
            stringsHeap.Read(typeDefRow.TypeName), stringsHeap.Read(typeDefRow.TypeNamespace));
        return type;
    }
}