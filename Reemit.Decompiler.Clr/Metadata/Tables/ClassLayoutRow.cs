using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class ClassLayoutRow : IMetadataTableRow
{
    public ushort PackingSize { get; private set; }
    public uint ClassSize { get; private set; }
    public uint Parent { get; private set; }

    public void Read(MetadataTableDataReader reader)
    {
        PackingSize = reader.ReadUInt16();
        ClassSize = reader.ReadUInt32();
        Parent = reader.ReadRidIntoTable(MetadataTableName.TypeDef);
    }
}