using Reemit.Common;

namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class MetadataTableDataReader(
    BinaryReader reader,
    HeapSizes heapSizes,
    IReadOnlyDictionary<MetadataTableName, uint> rowsCounts) : SharedReader(
    (reader as SharedReader)?.Offset ?? (int)reader.BaseStream.Position, reader)
{
    public uint ReadStringRid() => ReadRid(HeapSizes.StringStream);

    public uint ReadGuidRid() => ReadRid(HeapSizes.GuidStream);

    public uint ReadBlobRid() => ReadRid(HeapSizes.BlobStream);

    private uint ReadRid(HeapSizes heapSize) => ReadMappedRid(heapSize);

    public RangeMapped<uint> ReadMappedStringRid() => ReadMappedRid(HeapSizes.StringStream);

    public RangeMapped<uint> ReadMappedGuidRid() => ReadMappedRid(HeapSizes.GuidStream);

    public RangeMapped<uint> ReadMappedBlobRid() => ReadMappedRid(HeapSizes.BlobStream);

    private RangeMapped<uint> ReadMappedRid(HeapSizes heapSize) =>
        heapSizes.HasFlag(heapSize) ? ReadMappedUInt32() : ReadMappedUInt16().Cast<uint>();

    public CodedIndex ReadCodedRid(CodedIndexTagFamily family) => ReadMappedCodedRid(family);

    public RangeMapped<CodedIndex> ReadMappedCodedRid(CodedIndexTagFamily family)
    {
        using (var rangeScope = CreateRangeScope())
        {
            var codedIndex = new CodedIndex(this, family, rowsCounts);

            return rangeScope.ToRangeMapped(codedIndex);
        }
    }

    public uint ReadRidIntoTable(MetadataTableName tableName) => ReadMappedRidIntoTable(tableName);

    public RangeMapped<uint> ReadMappedRidIntoTable(MetadataTableName tableName) =>
        !rowsCounts.TryGetValue(tableName, out var value) || value <= ushort.MaxValue
            ? ReadMappedUInt16().Cast<uint>()
            : ReadMappedUInt32();
}