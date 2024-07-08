using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler.Clr.Metadata;

public class TableReferenceResolver(IReadOnlyDictionary<MetadataTableName, IReadOnlyList<IMetadataRecord>> allTables)
{
    public IReadOnlyList<TTarget> GetReferencedRows<TRef, TTarget>(TRef referencingRow, Func<TRef, uint> ridSelector)
        where TRef : IMetadataTableRow
        where TTarget : IMetadataTableRow =>
        CodedIndexExtensions.GetReferencedRows(referencingRow, ridSelector,
                allTables[TRef.TableName].OfType<TRef>().ToArray(),
                allTables[TTarget.TableName])
            .Cast<TTarget>()
            .ToArray()
            .AsReadOnly();
}

public static class CodedIndexExtensions
{
    public static IReadOnlyList<TTarget> GetReferencedRows<TRef, TTarget>(
        TRef referencingRow,
        Func<TRef, uint> ridSelector,
        IReadOnlyList<TRef> referencingTableRows,
        IReadOnlyList<TTarget> referencedTableRows)
        where TRef : IMetadataRecord
        where TTarget : IMetadataRecord
    {
        var nextRowInReferencingTable = referencingTableRows.SingleOrDefault(x => x.Rid - referencingRow.Rid == 1);

        var firstRowReferencedRid = ridSelector(referencingRow);

        int lastReferencedRowIndex;

        if (nextRowInReferencingTable is not null)
        {
            var nextRowReferencedRid = ridSelector(nextRowInReferencingTable);
            if (firstRowReferencedRid == nextRowReferencedRid)
            {
                return Array.Empty<TTarget>();
            }

            lastReferencedRowIndex = (int)ridSelector(nextRowInReferencingTable) - 1;
        }
        else
        {
            lastReferencedRowIndex = referencedTableRows.Count;
        }
        
        var firstReferencedRowIndex = (int)firstRowReferencedRid - 1;

        return referencedTableRows
            .Skip(firstReferencedRowIndex)
            .Take(lastReferencedRowIndex - firstReferencedRowIndex)
            .ToArray()
            .AsReadOnly();
    }
}