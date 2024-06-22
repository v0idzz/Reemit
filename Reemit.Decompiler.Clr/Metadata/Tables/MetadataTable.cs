namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class MetadataTable<TRow> where TRow : IMetadataTableRow<TRow>
{
    public IReadOnlyList<TRow> Rows { get; }

    public MetadataTable(uint rowCount, MetadataTableDataReader reader)
    {
        var rows = new List<TRow>((int) rowCount);

        for (var i = 0u; i < rowCount; i++)
        {
            rows.Add(TRow.Read(i + 1, reader));
        }

        Rows = rows.AsReadOnly();
    }
}