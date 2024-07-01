namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class ModuleRow(uint rid, byte[] generation, uint name, uint mvid, uint encId, uint encBaseId)
    : MetadataRecord(rid), IMetadataTableRow<ModuleRow>
{
    public static MetadataTableName TableName => MetadataTableName.Module;

    public byte[] Generation { get; } = generation;
    public uint Name { get; } = name;
    public uint Mvid { get; } = mvid;
    public uint EncId { get; } = encId;
    public uint EncBaseId { get; } = encBaseId;

    public static ModuleRow Read(uint rid, MetadataTableDataReader reader)
    {
        var generation = reader.ReadBytes(2);

        if (generation.Any(x => x != 0))
        {
            throw new BadImageFormatException($"{nameof(generation)} shall be zero.");
        }

        var name = reader.ReadStringRid();
        var mvid = reader.ReadGuidRid();
        var encId = reader.ReadGuidRid();

        if (encId != 0)
        {
            throw new BadImageFormatException($"{nameof(encId)} shall be zero.");
        }

        var encBaseId = reader.ReadGuidRid();

        if (encBaseId != 0)
        {
            throw new BadImageFormatException($"{nameof(encBaseId)} shall be zero.");
        }

        return new(rid, generation, name, mvid, encId, encBaseId);
    }
}