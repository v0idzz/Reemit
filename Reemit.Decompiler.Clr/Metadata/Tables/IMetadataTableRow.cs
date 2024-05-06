namespace Reemit.Decompiler.Cli.Metadata.Tables;

public interface IMetadataTableRow
{
    void Read(MetadataTableDataReader reader);
}