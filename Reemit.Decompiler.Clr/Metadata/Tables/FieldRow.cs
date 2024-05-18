namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class FieldRow : IMetadataTableRow
{
    public FieldAttributes Flags { get; private set; }
    public uint Name { get; private set; }
    public uint Signature { get; private set; }

    public void Read(MetadataTableDataReader reader)
    {
        Flags = (FieldAttributes)(reader.ReadUInt16() & FlagMasks.FieldAccessMask);
        Name = reader.ReadStringRid();
        Signature = reader.ReadBlobRid();
    }
}