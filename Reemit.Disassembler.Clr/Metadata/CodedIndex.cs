namespace Reemit.Disassembler.Clr.Metadata;

public class CodedIndex
{
    public uint ZeroBasedIndex => Rid - 1;
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
            },
            {
                CodedIndexTagFamily.MemberRefParent,
                [
                    MetadataTableName.TypeDef,
                    MetadataTableName.TypeRef,
                    MetadataTableName.ModuleRef,
                    MetadataTableName.MethodDef,
                    MetadataTableName.TypeSpec
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

        var read16Bits = maxNumberOfRows < Math.Pow(2, 16 - Math.Ceiling(Math.Log2(tags.Length)));
        var value = read16Bits 
            ? reader.ReadUInt16()
            : reader.ReadUInt32();

        var bitsToEncodeTag = (int)Math.Ceiling(Math.Log2(tags.Length));
        
        var mask = (uint)((1 << bitsToEncodeTag) - 1);
            
        var tag = value & mask;
        var rid = value >> bitsToEncodeTag;

        Rid = rid;
        ReferencedTable = MapTags[tagFamily][tag];
    }
}