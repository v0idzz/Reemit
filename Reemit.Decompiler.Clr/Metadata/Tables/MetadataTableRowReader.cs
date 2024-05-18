namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class MetadataTableDataReader(BinaryReader reader, HeapSizes heapSizes,
    IReadOnlyDictionary<MetadataTableName, uint> rowsCounts) : BinaryReader(reader.BaseStream)
{
    public uint ReadStringRid() => ReadRid(HeapSizes.StringStream);

    public uint ReadGuidRid() => ReadRid(HeapSizes.GuidStream);

    public uint ReadBlobRid() => ReadRid(HeapSizes.BlobStream);

    private uint ReadRid(HeapSizes heapSize) =>
        heapSizes.HasFlag(heapSize) ? reader.ReadUInt32() : reader.ReadUInt16();

    public CodedIndex ReadCodedRid(CodedIndexTagFamily family) => new(this, family, rowsCounts);

    public uint ReadRidIntoTable(MetadataTableName tableName) =>
        !rowsCounts.TryGetValue(tableName, out var value) || value <= ushort.MaxValue 
            ? reader.ReadUInt16() 
            : reader.ReadUInt32();
}