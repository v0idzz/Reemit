using Reemit.Common;

namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class ModuleRow(
    RangeMapped<byte[]> generation,
    RangeMapped<uint> name,
    RangeMapped<uint> mvid,
    RangeMapped<uint> encId,
    RangeMapped<uint> encBaseId)
    : IMetadataTableRow<ModuleRow>
{
    public static MetadataTableName TableName => MetadataTableName.Module;

    public RangeMapped<byte[]> Generation { get; } = generation;
    public RangeMapped<uint> Name { get; } = name;
    public RangeMapped<uint> Mvid { get; } = mvid;
    public RangeMapped<uint> EncId { get; } = encId;
    public RangeMapped<uint> EncBaseId { get; } = encBaseId;

    public static ModuleRow Read(MetadataTableDataReader reader)
    {
        var generation = reader.ReadMappedBytes(2);

        if (generation.Value.Any(x => x != 0))
        {
            throw new BadImageFormatException($"{nameof(generation)} shall be zero.");
        }

        var name = reader.ReadMappedStringRid();
        var mvid = reader.ReadMappedGuidRid();
        var encId = reader.ReadMappedGuidRid();

        if (encId != 0)
        {
            throw new BadImageFormatException($"{nameof(encId)} shall be zero.");
        }

        var encBaseId = reader.ReadMappedGuidRid();

        if (encBaseId != 0)
        {
            throw new BadImageFormatException($"{nameof(encBaseId)} shall be zero.");
        }

        return new(generation, name, mvid, encId, encBaseId);
    }
}