using Reemit.Common;

namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class MetadataTableDataReader(BinaryReader reader, HeapSizes heapSizes,
    IReadOnlyDictionary<MetadataTableName, uint> rowsCounts) : SharedReader((reader as SharedReader)?.Offset ?? 0, reader, (reader as SharedReader)?.SynchronizationObject ?? new object())
{
    public uint ReadStringRid() => ReadRid(HeapSizes.StringStream);

    public uint ReadGuidRid() => ReadRid(HeapSizes.GuidStream);

    public uint ReadBlobRid() => ReadRid(HeapSizes.BlobStream);

    private uint ReadRid(HeapSizes heapSize) =>
        heapSizes.HasFlag(heapSize) ? ReadUInt32() : ReadUInt16();

    public CodedIndex ReadCodedRid(CodedIndexTagFamily family) => new(this, family, rowsCounts);

    public uint ReadRidIntoTable(MetadataTableName tableName) =>
        !rowsCounts.TryGetValue(tableName, out var value) || value <= ushort.MaxValue 
            ? ReadUInt16() 
            : ReadUInt32();
}