using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler.Clr.Metadata;

public class TableReferenceResolver(IReadOnlyDictionary<MetadataTableName, IReadOnlyList<IMetadataRecord>> allTables)
{
    public IReadOnlyList<IMetadataRecord>
        GetReferencedRows<TRef>(CodedIndex codedIndex, TRef referencingRow)
        where TRef : IMetadataTableRow =>
        codedIndex.GetReferencedRows(referencingRow, allTables);

    public IReadOnlyList<TTarget> GetReferencedRows<TRef, TTarget>(TRef referencingRow)
        where TRef : IMetadataTableRow
        where TTarget : IMetadataTableRow =>
        CodedIndexExtensions.GetReferencedRows(referencingRow, allTables[TRef.TableName],
                allTables[TTarget.TableName])
            .Cast<TTarget>()
            .ToArray()
            .AsReadOnly();
}

public static class CodedIndexExtensions
{
    public static IReadOnlyList<IMetadataRecord> GetReferencedRows<TRef>(this CodedIndex codedIndex,
        TRef referencingRow,
        IReadOnlyDictionary<MetadataTableName, IReadOnlyList<IMetadataRecord>> allTables)
        where TRef : IMetadataTableRow =>
        GetReferencedRows(referencingRow, allTables[TRef.TableName], allTables[codedIndex.ReferencedTable]);

    public static IReadOnlyList<TTarget> GetReferencedRows<TRef, TTarget>(TRef referencingRow,
        IReadOnlyList<TRef> referencingTableRows,
        IReadOnlyList<TTarget> referencedTableRows)
        where TRef : IMetadataRecord
        where TTarget : IMetadataRecord
    {
        var nextRowInReferencingTable = referencingTableRows.SingleOrDefault(x => x.Rid - referencingRow.Rid == 1);

        var firstRowIndex = (int)(referencingRow.Rid - 1);
        var lastRowIndex = (int?)(nextRowInReferencingTable?.Rid - 1) ?? referencedTableRows.Count - 1;

        return referencedTableRows
            .Skip(firstRowIndex)
            .Take(lastRowIndex)
            .ToArray()
            .AsReadOnly();
    }
}