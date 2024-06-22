using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler.Clr.Metadata;

public class TableReferenceResolver(IReadOnlyDictionary<MetadataTableName, IReadOnlyList<IMetadataTableRow>> allTables)
{
    public IReadOnlyList<IMetadataTableRow>
        GetReferencedRows(CodedIndex codedIndex, IMetadataTableRow referencingRow) =>
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
    public static IReadOnlyList<IMetadataTableRow> GetReferencedRows<TRef>(this CodedIndex codedIndex,
        TRef referencingRow,
        IReadOnlyDictionary<MetadataTableName, IReadOnlyList<IMetadataTableRow>> allTables)
        where TRef : IMetadataTableRow =>
        GetReferencedRows(referencingRow, allTables[TRef.TableName], allTables[codedIndex.ReferencedTable]);

    public static IReadOnlyList<TTarget> GetReferencedRows<TRef, TTarget>(TRef referencingRow,
        IReadOnlyList<TRef> referencingTableRows,
        IReadOnlyList<TTarget> referencedTableRows)
        where TRef : IMetadataTableRow
        where TTarget : IMetadataTableRow
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