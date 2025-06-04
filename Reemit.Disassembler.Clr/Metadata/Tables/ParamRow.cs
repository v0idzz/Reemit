namespace Reemit.Disassembler.Clr.Metadata.Tables;

public class ParamRow(uint rid, ParamAttributes flags, ushort sequence, uint name)
    : MetadataRecord(rid), IMetadataTableRow<ParamRow>
{
    public static MetadataTableName TableName => MetadataTableName.Param;

    public ParamAttributes Flags => flags;
    public ushort Sequence => sequence;
    public uint Name => name;

    public static ParamRow Read(uint rid, MetadataTableDataReader reader) =>
        new(rid, (ParamAttributes)reader.ReadUInt16(), reader.ReadUInt16(), reader.ReadStringRid());
}