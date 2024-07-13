using Reemit.Decompiler.Clr.Metadata;
using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata;

public class TableReferenceResolverTests
{
    [Fact]
    public void GetReferencedRows_RecordReferencingContiguousRunOfRecordsUntilNextRowRun_ReturnsRunOfRecords()
    {
        // Arrange
        var (referencingRecords, referencedRecords, tableReferenceResolver) = SetupTestData(
            new (uint, uint)[] { (1, 1), (2, 4) },
            [1, 2, 3, 4, 5]);

        // Act
        var result = tableReferenceResolver.GetReferencedRows<ReferencingRecord, ReferencedRecord>(
            referencingRecords[0],
            x => x.ReferencedRowRid);

        // Assert
        Assert.Equal(referencedRecords[..3], result);
    }

    [Fact]
    public void
        GetReferencedRows_RecordReferencingContiguousRunOfRecordsUntilEndOfTable_ReturnsRunOfRecordsUntilEndOfTable()
    {
        // Arrange
        var (referencingRecords, referencedRecords, tableReferenceResolver) = SetupTestData(
            new (uint, uint)[] { (1, 1), (2, 4) },
            [1, 2, 3, 4, 5]);

        // Act
        var result = tableReferenceResolver.GetReferencedRows<ReferencingRecord, ReferencedRecord>(
            referencingRecords[1],
            x => x.ReferencedRowRid);

        // Assert
        Assert.Equal(referencedRecords[3..], result);
    }

    [Fact]
    public void GetReferencedRows_RecordReferencingSameRowAsNextRow_ReturnsEmptyCollection()
    {
        // Arrange
        var (referencingRecords, _, tableReferenceResolver) = SetupTestData(
            new (uint, uint)[] { (1, 1), (2, 1) },
            [1, 2]);

        // Act
        var result = tableReferenceResolver.GetReferencedRows<ReferencingRecord, ReferencedRecord>(
            referencingRecords[0],
            x => x.ReferencedRowRid);

        // Assert
        Assert.Empty(result);
    }

    private static (ReferencingRecord[], ReferencedRecord[], TableReferenceResolver) SetupTestData(
        (uint Rid, uint ReferencedRowRid)[] referencingRowsData,
        uint[] referencedRecordRids)
    {
        var referencingRecords = referencingRowsData
            .Select(data => new ReferencingRecord(data.Rid, data.ReferencedRowRid))
            .ToArray();

        var referencedRecords = referencedRecordRids
            .Select(rid => new ReferencedRecord(rid))
            .ToArray();

        var irrelevantRecords = new[] { new IrrelevantRecord(1), new IrrelevantRecord(2) };

        var allTables = new Dictionary<MetadataTableName, IReadOnlyList<IMetadataRecord>>
        {
            { ReferencingRecord.TableName, referencingRecords },
            { ReferencedRecord.TableName, referencedRecords },
            { IrrelevantRecord.TableName, irrelevantRecords }
        };

        var tableReferenceResolver = new TableReferenceResolver(allTables);

        return (referencingRecords, referencedRecords, tableReferenceResolver);
    }

    private record ReferencingRecord(uint Rid, uint ReferencedRowRid) : IMetadataTableRow<ReferencingRecord>
    {
        public static ReferencingRecord Read(uint rid, MetadataTableDataReader reader) =>
            throw new NotImplementedException();

        public static MetadataTableName TableName => MetadataTableName.File;
    }

    private record ReferencedRecord(uint Rid) : IMetadataTableRow<ReferencedRecord>
    {
        public static ReferencedRecord Read(uint rid, MetadataTableDataReader reader) =>
            throw new NotImplementedException();

        public static MetadataTableName TableName => MetadataTableName.Assembly;
    }

    private record IrrelevantRecord(uint Rid) : IMetadataTableRow<IrrelevantRecord>
    {
        public static IrrelevantRecord Read(uint rid, MetadataTableDataReader reader) =>
            throw new NotImplementedException();

        public static MetadataTableName TableName => MetadataTableName.Param;
    }
}