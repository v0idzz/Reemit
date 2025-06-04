namespace Reemit.Disassembler.Clr.Metadata.Tables;

public class TypeDefRow(
    uint rid,
    uint flags,
    uint typeName,
    uint typeNamespace,
    CodedIndex extends,
    uint fieldList,
    uint methodList)
    : MetadataRecord(rid), IMetadataTableRow<TypeDefRow>
{
    public static MetadataTableName TableName => MetadataTableName.TypeDef;

    public uint Flags { get; } = flags;
    public uint TypeName { get; } = typeName;
    public uint TypeNamespace { get; } = typeNamespace;
    public CodedIndex Extends { get; } = extends;
    public uint FieldList { get; } = fieldList;
    public uint MethodList { get; } = methodList;

    public TypeClassLayoutAttributes ClassLayout =>
        (TypeClassLayoutAttributes)(Flags & (short)TypeClassLayoutAttributes.Mask);
    
    public TypeClassSemanticsAttributes ClassSemantics =>
        (TypeClassSemanticsAttributes)(Flags & (short)TypeClassSemanticsAttributes.Mask);

    public TypeImplementationAttributes Implementation =>
        (TypeImplementationAttributes)(Flags & (short)TypeImplementationAttributes.Mask);

    public TypeStringFormattingAttributes StringFormatting =>
        (TypeStringFormattingAttributes)(Flags & (uint)TypeStringFormattingAttributes.Mask);

    public TypeVisibilityAttributes Visibility =>
        (TypeVisibilityAttributes)(Flags & (uint)TypeVisibilityAttributes.Mask);

    public static TypeDefRow Read(uint rid, MetadataTableDataReader reader)
    {
        var flags = reader.ReadUInt32();
        var invalidFlags = flags & ~FlagMasks.TypeAttributesMask;
        
        if (invalidFlags != 0x0)
        {
            throw new BadImageFormatException("Invalid TypeAttributes flags");
        }

        return new TypeDefRow(
            rid,
            flags,
            reader.ReadStringRid(),
            reader.ReadStringRid(),
            reader.ReadCodedRid(CodedIndexTagFamily.TypeDefOrRef),
            reader.ReadRidIntoTable(MetadataTableName.Field),
            reader.ReadRidIntoTable(MetadataTableName.MethodDef));
    }
}