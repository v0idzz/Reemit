namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class MetadataTable<TRow> where TRow : IMetadataTableRow, new()
{
    public IReadOnlyList<TRow> Rows { get; }

    public MetadataTable(uint rowCount, MetadataTableDataReader reader)
    {
        var rows = new List<TRow>((int) rowCount);

        for (var i = 0; i < rowCount; i++)
        {
            var row = new TRow();
            row.Read(reader);

            rows.Add(row);
        }

        Rows = rows.AsReadOnly();
    }
}