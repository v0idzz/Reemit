using System.Collections;
using System.Collections.Immutable;
using Reemit.Common;
using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler.Clr.Metadata.Streams;

public class MetadataTablesStream
{
    public const string Name = "#~";

    public RangeMapped<uint> Reserved { get; }
    public RangeMapped<byte> MajorVersion { get; }
    public RangeMapped<byte> MinorVersion { get; }
    public RangeMapped<HeapSizes> HeapSizes { get; }
    public RangeMapped<byte> Reserved1 { get; }

    // Not sure how far we want to go with range mapping; these
    // are comprised of mapped rows already.
    public MetadataTable<ModuleRow> Module { get; }
    public MetadataTable<TypeRefRow>? TypeRef { get; }
    public MetadataTable<TypeDefRow>? TypeDef { get; }
    public MetadataTable<FieldRow>? Field { get; }

    private readonly Dictionary<MetadataTableName, RangeMapped<uint>> _rowsCounts;
    private readonly MetadataTableDataReader _metadataTableDataReader;

    public MetadataTablesStream(SharedReader reader)
    {
        Reserved = reader.ReadMappedUInt32();
        MajorVersion = reader.ReadMappedByte();
        MinorVersion = reader.ReadMappedByte();
        HeapSizes = reader.ReadMappedByte().Cast<HeapSizes>();
        Reserved1 = reader.ReadMappedByte();

        // Todo: track range of validBits?
        var validBits = new BitArray(reader.ReadBytes(8)).OfType<bool>().ToArray();

        // var sortedBits = new BitArray(
        reader.ReadMappedBytes(8);

        _rowsCounts = new Dictionary<MetadataTableName, RangeMapped<uint>>(validBits.Count(x => x));

        foreach (var name in validBits
            .Select((x, i) => (IsValid: x, TableName: (MetadataTableName)i))
            .Where(x => x.IsValid)
            .Select(X => X.TableName))
        {
            // Even though this is not exposed, reading these as mapped in case
            // this is a value we want to expose and make navigable in the future
            _rowsCounts[name] = reader.ReadMappedUInt32();
        }

        _metadataTableDataReader = new MetadataTableDataReader(
            reader,
            HeapSizes,
            _rowsCounts.ToImmutableDictionary(x => x.Key, x => x.Value.Value));

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
    }

    private MetadataTable<T>? ReadTableIfExists<T>() where T : IMetadataTableRow<T> =>
        !_rowsCounts.TryGetValue(T.TableName, out var count)
            ? null
            : new MetadataTable<T>(count, _metadataTableDataReader);
}