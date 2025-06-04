namespace Reemit.Disassembler.Clr.Metadata.Tables;

public class FieldRow(uint rid, FieldAttributes flags, uint name, uint signature)
    : MetadataRecord(rid), IMetadataTableRow<FieldRow>
{
    public static MetadataTableName TableName => MetadataTableName.Field;

    public FieldAttributes Flags { get; } = flags;
    public uint Name { get; } = name;
    public uint Signature { get; } = signature;

    public static FieldRow Read(uint rid, MetadataTableDataReader reader) =>
        new(
            rid,
            (FieldAttributes)(reader.ReadUInt16() & FlagMasks.FieldAccessMask),
            reader.ReadStringRid(),
            reader.ReadBlobRid());
}