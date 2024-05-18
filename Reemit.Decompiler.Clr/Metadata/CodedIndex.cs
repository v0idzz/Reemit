namespace Reemit.Decompiler.Clr.Metadata;

public class CodedIndex
{
    public uint Rid { get; }
    public MetadataTableName ReferencedTable { get; }

    private static readonly Dictionary<CodedIndexTagFamily, MetadataTableName[]> MapTags =
        new()
        {
            {
                CodedIndexTagFamily.TypeDefOrRef,
                [
                    MetadataTableName.TypeDef,
                    MetadataTableName.TypeRef,
                    MetadataTableName.TypeSpec
                ]
            },
            {
                CodedIndexTagFamily.HasConstant,
                [
                    MetadataTableName.Field,
                    MetadataTableName.Param,
                    MetadataTableName.Property
                ]
            },
            {
                CodedIndexTagFamily.ResolutionScope,
                [
                    MetadataTableName.Module,
                    MetadataTableName.ModuleRef,
                    MetadataTableName.AssemblyRef,
                    MetadataTableName.TypeRef
                ]
            }
        };
    
    public CodedIndex(BinaryReader reader,
        CodedIndexTagFamily tagFamily,
        IReadOnlyDictionary<MetadataTableName, uint> rowsCounts)
    {
        var tags = MapTags[tagFamily];

        var maxNumberOfRows = rowsCounts.Where(x => tags.Contains(x.Key))
            .Select(x => x.Value)
            .Max();

        var read16Bits = maxNumberOfRows < Math.Pow(2, 16 - Math.Log2(tags.Length));
        var value = read16Bits 
            ? reader.ReadUInt16()
            : reader.ReadUInt32();

        var bitsToEncodeTag = (int)Math.Floor(Math.Log2(tags.Length - 1) + 1);
        var mask = (uint)((1 << bitsToEncodeTag) - 1);
            
        var tag = value & mask;
        var rid = value >> bitsToEncodeTag;

        Rid = rid;
        ReferencedTable = MapTags[tagFamily][tag];
    }
}