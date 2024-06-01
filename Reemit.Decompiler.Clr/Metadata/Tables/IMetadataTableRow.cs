namespace Reemit.Decompiler.Clr.Metadata.Tables;

public interface IMetadataTableRow
{
    static abstract MetadataTableName TableName { get; }
}

public interface IMetadataTableRow<T> : IMetadataTableRow
    where T : IMetadataTableRow<T>
{
    static abstract T Read(MetadataTableDataReader reader);
}