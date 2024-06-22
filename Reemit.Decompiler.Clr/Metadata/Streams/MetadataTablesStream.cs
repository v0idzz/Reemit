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

    public MetadataTable<ModuleRow> Module { get; }
    public MetadataTable<TypeRefRow>? TypeRef { get; }
    public MetadataTable<TypeDefRow>? TypeDef { get; }
    public MetadataTable<FieldRow>? Field { get; }
    public MetadataTable<MethodDefRow>? MethodDef { get; }

    public IReadOnlyDictionary<MetadataTableName, IReadOnlyList<IMetadataTableRow>> Rows => _rows
        .ToDictionary(x => x.Key, x => (IReadOnlyList<IMetadataTableRow>)x.Value.ToArray().AsReadOnly())
        .AsReadOnly();

    private readonly Dictionary<MetadataTableName, List<IMetadataTableRow>> _rows;
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

        _rows = new Dictionary<MetadataTableName, List<IMetadataTableRow>>(validBits.Count(x => x));

        foreach (var name in validBits
                     .Select((x, i) => (IsValid: x, TableName: (MetadataTableName)i))
                     .Where(x => x.IsValid)
                     .Select(x => x.TableName))
        {
            _rows[name] = new List<IMetadataTableRow>((int)reader.ReadUInt32());
        }

        _metadataTableDataReader = new MetadataTableDataReader(reader, HeapSizes,
            _rows.ToDictionary(x => x.Key, x => (uint)x.Value.Capacity));

        var moduleTable = ReadTableIfExists<ModuleRow>();

        // From II.22.30, informative text entry 1: The Module table shall contain one and only one row [ERROR]
        if (moduleTable is not { Rows.Count: 1 })
        {
            throw new BadImageFormatException("The Module table shall contain one and only one row");
        }

        Module = moduleTable;

        TypeRef = ReadTableIfExists<TypeRefRow>();
        TypeDef = ReadTableIfExists<TypeDefRow>();
        Field = ReadTableIfExists<FieldRow>();
        MethodDef = ReadTableIfExists<MethodDefRow>();
    }

    private MetadataTable<T>? ReadTableIfExists<T>() where T : IMetadataTableRow<T>
    {
        if (!_rows.TryGetValue(T.TableName, out var list))
        {
            return null;
        }

        var table = new MetadataTable<T>((uint)list.Capacity, _metadataTableDataReader);
        _rows[T.TableName].AddRange(table.Rows.Cast<IMetadataTableRow>());

        return table;
    }
}