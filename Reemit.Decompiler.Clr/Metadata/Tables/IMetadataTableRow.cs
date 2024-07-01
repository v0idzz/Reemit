namespace Reemit.Decompiler.Clr.Metadata.Tables;

public interface IMetadataTableRow : IMetadataRecord
{
    static abstract MetadataTableName TableName { get; }
}

public interface IMetadataTableRow<out T> : IMetadataTableRow
    where T : IMetadataTableRow<T>
{
    static abstract T Read(uint rid, MetadataTableDataReader reader);
}