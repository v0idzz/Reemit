namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class ExportedTypeRow : IMetadataTableRow
{
    public TypeAttributes Flags { get; private set; }
    public uint TypeDefId { get; private set; }
    public uint TypeName { get; private set; }
    public uint TypeNamespace { get; private set; }
    public CodedIndex Implementation { get; private set; } = null!;

    public void Read(MetadataTableDataReader reader)
    {
        Flags = (TypeAttributes)(reader.ReadUInt32() & FlagMasks.TypeAttributesMask);
        TypeDefId = reader.ReadRidIntoTable(MetadataTableName.TypeDef);
        TypeName = reader.ReadStringRid();
        TypeNamespace = reader.ReadStringRid();
        Implementation = reader.ReadCodedRid(CodedIndexTagFamily.Implementation);
    }
}