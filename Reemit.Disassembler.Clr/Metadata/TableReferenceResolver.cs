using Reemit.Disassembler.Clr.Metadata.Tables;

namespace Reemit.Disassembler.Clr.Metadata;

public class TableReferenceResolver(IReadOnlyDictionary<MetadataTableName, IReadOnlyList<IMetadataRecord>> allTables)
{
    public IReadOnlyList<TTarget> GetReferencedRows<TRef, TTarget>(TRef referencingRow, Func<TRef, uint> ridSelector)
        where TRef : IMetadataTableRow
        where TTarget : IMetadataTableRow
    {
        if (!allTables.ContainsKey(TTarget.TableName))
        {
            return Array.Empty<TTarget>();
        }

        return CodedIndexExtensions.GetReferencedRows(referencingRow, ridSelector,
                allTables[TRef.TableName].OfType<TRef>().ToArray(),
                allTables[TTarget.TableName])
            .Cast<TTarget>()
            .ToArray()
            .AsReadOnly();
    }

    public T GetReferencedRow<T>(uint rid)
        where T : IMetadataTableRow =>
        (T)allTables[T.TableName].Single(x => x.Rid == rid);
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

        int lastReferencedRowNo;

        if (nextRowInReferencingTable is not null)
        {
            var nextRowReferencedRid = ridSelector(nextRowInReferencingTable);
            if (firstRowReferencedRid == nextRowReferencedRid)
            {
                return Array.Empty<TTarget>();
            }

            lastReferencedRowNo = (int)ridSelector(nextRowInReferencingTable) - 1;
        }
        else
        {
            lastReferencedRowNo = referencedTableRows.Count;
        }

        var firstReferencedRowIndex = (int)firstRowReferencedRid - 1;

        return referencedTableRows
            .Skip(firstReferencedRowIndex)
            .Take(lastReferencedRowNo - firstReferencedRowIndex)
            .ToArray()
            .AsReadOnly();
    }
}