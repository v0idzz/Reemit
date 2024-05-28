namespace Reemit.Decompiler.Clr.Metadata.Tables;

public interface IMetadataTableRow
{
    static abstract MetadataTableName TableName { get; }

    void Read(MetadataTableDataReader reader);
}