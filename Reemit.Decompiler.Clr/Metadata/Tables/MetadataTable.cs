using Reemit.Common;

namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class MetadataTable<TRow> where TRow : IMetadataTableRow<TRow>
{
    public IReadOnlyList<RangeMapped<TRow>> Rows { get; }

    public MetadataTable(uint rowCount, MetadataTableDataReader reader)
    {
        var rows = new List<RangeMapped<TRow>>((int) rowCount);

        for (var i = 0; i < rowCount; i++)
        {
            using (var rangeScope = reader.CreateRangeScope())
            {
                var row = TRow.Read(reader);
                var rangeMappedRow = rangeScope.ToRangeMapped(row);
                rows.Add(rangeMappedRow);
            }
        }

        Rows = rows.AsReadOnly();
    }
}