using System.Collections;
using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler.Clr.Metadata.Streams;

public class MetadataTablesStream
{
    public const string Name = "#~";

    public uint Reserved { get; }
    public byte MajorVersion { get; }
    public byte MinorVersion { get; }
    public HeapSizes HeapSizes { get; }
    public byte Reserved1 { get; }

    public MetadataTable<ModuleRow>? Module { get; }
    public MetadataTable<TypeRefRow>? TypeRef { get; }
    public MetadataTable<TypeDefRow>? TypeDef { get; }
    public MetadataTable<FieldRow>? Field { get; }

    private readonly Dictionary<MetadataTableName, uint> _rowsCounts;
    private readonly MetadataTableDataReader _metadataTableDataReader;

    public MetadataTablesStream(BinaryReader reader)
    {
        Reserved = reader.ReadUInt32();
        MajorVersion = reader.ReadByte();
        MinorVersion = reader.ReadByte();
        HeapSizes = (HeapSizes)reader.ReadByte();
        Reserved1 = reader.ReadByte();
        var validBits = new BitArray(reader.ReadBytes(8)).OfType<bool>().ToArray();
        
        // var sortedBits = new BitArray(
        reader.ReadBytes(8);

        _rowsCounts = new Dictionary<MetadataTableName, uint>(validBits.Count(x => x));

        foreach (var (member, index) in Enum.GetValues<MetadataTableName>()
                     .OrderBy(x => (int)x)
                     .Select((m, i) => (m, i)))
        {
            if (validBits[index])
            {
                _rowsCounts[member] = reader.ReadUInt32();
            }
        }

        _metadataTableDataReader = new MetadataTableDataReader(reader, HeapSizes, _rowsCounts);

        Module = ReadTableIfExists<ModuleRow>();
        TypeRef = ReadTableIfExists<TypeRefRow>();
        TypeDef = ReadTableIfExists<TypeDefRow>();
        Field = ReadTableIfExists<FieldRow>();
    }

    private MetadataTable<T>? ReadTableIfExists<T>() where T : IMetadataTableRow, new() =>
        !_rowsCounts.TryGetValue(T.TableName, out var count)
            ? null
            : new MetadataTable<T>(count, _metadataTableDataReader);
}