namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class TypeRefRow(CodedIndex resolutionScope, uint typeName, uint typeNamespace)
    : IMetadataTableRow<TypeRefRow>
{
    public static MetadataTableName TableName => MetadataTableName.TypeRef;

    public CodedIndex ResolutionScope { get; } = resolutionScope;
    public uint TypeName { get; } = typeName;
    public uint TypeNamespace { get; } = typeNamespace;

    public static TypeRefRow Read(MetadataTableDataReader reader) =>
        new(
            reader.ReadCodedRid(CodedIndexTagFamily.ResolutionScope),
            reader.ReadStringRid(),
            reader.ReadStringRid());
}