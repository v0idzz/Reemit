namespace Reemit.Decompiler.Clr.Metadata.Tables;

public abstract class MetadataRecord : IMetadataRecord
{
    protected MetadataRecord(uint rid)
    {
        if (rid == 0)
        {
            throw new ArgumentException("Invalid RID, expected number greater than 0", nameof(rid));
        }

        Rid = rid;
    }

    public uint Rid { get; }
}