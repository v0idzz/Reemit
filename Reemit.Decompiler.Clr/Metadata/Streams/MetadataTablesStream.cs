using System.Collections;
using Reemit.Decompiler.Cli.Metadata.Tables;

namespace Reemit.Decompiler.Cli.Metadata.Streams;

public class MetadataTablesStream
{
    public uint Reserved { get; }
    public byte MajorVersion { get; }
    public byte MinorVersion { get; }
    public HeapSizes HeapSizes { get; }
    public byte Reserved1 { get; }
    
    public MetadataTable<ModuleRow>? Module { get; }
    public MetadataTable<TypeRefRow>? TypeRef { get; }
    public MetadataTable<TypeDefRow> TypeDef { get; }
    
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

        var rowsCounts = new Dictionary<MetadataTableName, uint>(validBits.Count(x => x));

        foreach (var (member, index) in Enum.GetValues<MetadataTableName>()
                     .OrderBy(x => (int)x)
                     .Select((m, i) => (m, i)))
        {
            if (validBits[index])
            {
                rowsCounts[member] = reader.ReadUInt32();
            }
        }

        var tableReader = new MetadataTableDataReader(reader, HeapSizes, rowsCounts);

        if (validBits[0])
        {
            Module = new MetadataTable<ModuleRow>(rowsCounts[MetadataTableName.Module], tableReader);
        }

        if (validBits[1])
        {
            TypeRef = new MetadataTable<TypeRefRow>(rowsCounts[MetadataTableName.TypeRef], tableReader);
        }

        if (validBits[2])
        {
            TypeDef = new MetadataTable<TypeDefRow>(rowsCounts[MetadataTableName.TypeDef], tableReader);
        }
    }
}