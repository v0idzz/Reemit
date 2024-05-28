namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class ModuleRow : IMetadataTableRow
{
    public static MetadataTableName TableName => MetadataTableName.Module;

    public byte[] Generation { get; private set; } = null!;
    public uint Name { get; private set; }
    public uint Mvid { get; private set; }
    public uint EncId { get; private set; }
    public uint EncBaseId { get; private set; }

    public void Read(MetadataTableDataReader reader)
    {
        Generation = reader.ReadBytes(2);

        if (Generation.Any(x => x != 0))
        {
            throw new BadImageFormatException($"{nameof(Generation)} shall be zero.");
        }
        
        Name = reader.ReadStringRid();
        Mvid = reader.ReadGuidRid();
        EncId = reader.ReadGuidRid();

        if (EncId != 0)
        {
            throw new BadImageFormatException($"{nameof(EncId)} shall be zero.");
        }
        
        EncBaseId = reader.ReadGuidRid();

        if (EncBaseId != 0)
        {
            throw new BadImageFormatException($"{nameof(EncBaseId)} shall be zero.");
        }
    }
}