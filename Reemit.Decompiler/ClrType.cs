using Reemit.Decompiler.Clr.Metadata;
using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler;

public class ClrType(bool isInterface, string name, string @namespace)
{
    public bool IsInterface => isInterface;
    public string Name => name;
    public string Namespace => @namespace;

    public static ClrType FromTypeDefRow(TypeDefRow typeDefRow, ClrMetadataContext context)
    {
        var stringsHeap = context.StringsHeapStream;
        // TODO: Check if also derives ultimately from System.Object
        var isInterface = typeDefRow.ClassSemantics == TypeClassSemanticsAttributes.Interface;
        var type = new ClrType(isInterface,
            stringsHeap.Read(typeDefRow.TypeName), stringsHeap.Read(typeDefRow.TypeNamespace));
        return type;
    }
}