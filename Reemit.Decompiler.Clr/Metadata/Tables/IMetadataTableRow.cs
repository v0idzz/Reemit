namespace Reemit.Decompiler.Clr.Metadata.Tables;

public abstract class MetadataTableRow<T> : IMetadataTableRow<T> where T : IMetadataTableRow<T>
{
    protected MetadataTableRow(uint rid)
    {
        if (rid == 0)
        {
            throw new ArgumentException("Invalid RID, expected number greater than 0", nameof(rid));
        }

        Rid = rid;
    }

    public uint Rid { get; }
}

public interface IMetadataTableRow
{
    uint Rid { get; }

    static virtual MetadataTableName TableName => throw new NotImplementedException();
}

public interface IMetadataTableRow<out T> : IMetadataTableRow
    where T : IMetadataTableRow<T>
{
    static virtual T Read(uint rid, MetadataTableDataReader reader) => throw new NotImplementedException();
}