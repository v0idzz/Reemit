using Reemit.Decompiler.Clr.Metadata;
using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata;

public class TableReferenceResolverTests
{
    [Fact]
    public void GetReferencedRows_RecordReferencingContiguousRunOfRecordsUntilNextRowRun_ReturnsRunOfRecords()
    {
        // Arrange
        ReferencingRecord[] referencingRecords =
        [
            new ReferencingRecord(1, 1),
            new ReferencingRecord(2, 4)
        ];

        ReferencedRecord[] referencedRecords =
        [
            new ReferencedRecord(1),
            new ReferencedRecord(2),
            new ReferencedRecord(3),
            new ReferencedRecord(4),
            new ReferencedRecord(5),
        ];

        IrrelevantRecord[] irrelevantRecords = [new IrrelevantRecord(1), new IrrelevantRecord(2)];

        var allTables = new Dictionary<MetadataTableName, IReadOnlyList<IMetadataRecord>>
        {
            { ReferencingRecord.TableName, referencingRecords },
            { ReferencedRecord.TableName, referencedRecords },
            { IrrelevantRecord.TableName, irrelevantRecords }
        };

        var tableReferenceResolver = new TableReferenceResolver(allTables);

        // Act
        var result = tableReferenceResolver.GetReferencedRows<ReferencingRecord, ReferencedRecord>(
            referencingRecords[0],
            x => x.ReferencedTableRid);

        // Assert
        Assert.Equal(referencedRecords[..3], result);
    }

    [Fact]
    public void
        GetReferencedRows_RecordReferencingContiguousRunOfRecordsUntilEndOfTable_ReturnsRunOfRecordsUntilEndOfTable()
    {
        // Arrange
        ReferencingRecord[] referencingRecords =
        [
            new ReferencingRecord(1, 1),
            new ReferencingRecord(2, 4)
        ];

        ReferencedRecord[] referencedRecords =
        [
            new ReferencedRecord(1),
            new ReferencedRecord(2),
            new ReferencedRecord(3),
            new ReferencedRecord(4),
            new ReferencedRecord(5),
        ];

        IrrelevantRecord[] irrelevantRecords = [new IrrelevantRecord(1), new IrrelevantRecord(2)];

        var allTables = new Dictionary<MetadataTableName, IReadOnlyList<IMetadataRecord>>
        {
            { ReferencingRecord.TableName, referencingRecords },
            { ReferencedRecord.TableName, referencedRecords },
            { IrrelevantRecord.TableName, irrelevantRecords }
        };

        var tableReferenceResolver = new TableReferenceResolver(allTables);

        // Act
        var result = tableReferenceResolver.GetReferencedRows<ReferencingRecord, ReferencedRecord>(
            referencingRecords[1],
            x => x.ReferencedTableRid);

        // Assert
        Assert.Equal(referencedRecords[3..], result);
    }

    private record ReferencingRecord(uint Rid, uint ReferencedTableRid) : IMetadataTableRow<ReferencingRecord>
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