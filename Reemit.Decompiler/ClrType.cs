using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler;

public class ClrType(string name, string @namespace)
{
    public string Name { get; } = name;
    public string Namespace { get; } = @namespace;

    public static ClrType FromTypeDefRow(TypeDefRow typeDefRow, ClrMetadataContext context)
    {
        var stringsHeap = context.StringsHeapStream;
        return new ClrType(stringsHeap.Read(typeDefRow.TypeName), stringsHeap.Read(typeDefRow.TypeNamespace));
    }
}