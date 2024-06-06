using Reemit.Common;

namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class FieldRow(RangeMapped<FieldAttributes> flags, RangeMapped<uint> name, RangeMapped<uint> signature)
    : IMetadataTableRow<FieldRow>
{
    public static MetadataTableName TableName => MetadataTableName.Field;

    public RangeMapped<FieldAttributes> Flags { get; } = flags;
    public RangeMapped<uint> Name { get; } = name;
    public RangeMapped<uint> Signature { get; } = signature;

    public static FieldRow Read(MetadataTableDataReader reader) =>
        new(
            reader
                .ReadMappedUInt16()
                .Select(x => (FieldAttributes)(x & FlagMasks.FieldAccessMask)),
            reader.ReadMappedStringRid(),
            reader.ReadMappedBlobRid());
}