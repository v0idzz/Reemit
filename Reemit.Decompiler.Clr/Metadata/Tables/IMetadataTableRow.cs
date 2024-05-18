namespace Reemit.Decompiler.Clr.Metadata.Tables;

public interface IMetadataTableRow
{
    void Read(MetadataTableDataReader reader);
}