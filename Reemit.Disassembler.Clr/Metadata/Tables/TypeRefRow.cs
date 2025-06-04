namespace Reemit.Disassembler.Clr.Metadata.Tables;

public class TypeRefRow(uint rid, CodedIndex resolutionScope, uint typeName, uint typeNamespace)
    : MetadataRecord(rid), IMetadataTableRow<TypeRefRow>
{
    public static MetadataTableName TableName => MetadataTableName.TypeRef;

    public CodedIndex ResolutionScope { get; } = resolutionScope;
    public uint TypeName { get; } = typeName;
    public uint TypeNamespace { get; } = typeNamespace;

    public static TypeRefRow Read(uint rid, MetadataTableDataReader reader) =>
        new(
            rid,
            reader.ReadCodedRid(CodedIndexTagFamily.ResolutionScope),
            reader.ReadStringRid(),
            reader.ReadStringRid());
}