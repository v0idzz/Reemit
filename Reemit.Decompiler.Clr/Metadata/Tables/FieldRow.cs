namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class FieldRow(FieldAttributes flags, uint name, uint signature)
    : IMetadataTableRow<FieldRow>
{
    public static MetadataTableName TableName => MetadataTableName.Field;

    public FieldAttributes Flags { get; } = flags;
    public uint Name { get; } = name;
    public uint Signature { get; } = signature;

    public static FieldRow Read(MetadataTableDataReader reader) =>
        new(
            (FieldAttributes)(reader.ReadUInt16() & FlagMasks.FieldAccessMask),
            reader.ReadStringRid(),
            reader.ReadBlobRid());
}