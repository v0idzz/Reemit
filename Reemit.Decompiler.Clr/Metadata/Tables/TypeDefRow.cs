namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class TypeDefRow(
    TypeAttributes flags,
    uint typeName,
    uint typeNamespace,
    CodedIndex extends,
    uint fieldList,
    uint methodList)
    : IMetadataTableRow<TypeDefRow>
{
    public static MetadataTableName TableName => MetadataTableName.TypeDef;

    public TypeAttributes Flags { get; } = flags;
    public uint TypeName { get; } = typeName;
    public uint TypeNamespace { get; } = typeNamespace;
    public CodedIndex Extends { get; } = extends;
    public uint FieldList { get; } = fieldList;
    public uint MethodList { get; } = methodList;

    public static TypeDefRow Read(MetadataTableDataReader reader) =>
        new(
            (TypeAttributes)(reader.ReadUInt32() & FlagMasks.TypeAttributesMask),
            reader.ReadStringRid(),
            reader.ReadStringRid(),
            reader.ReadCodedRid(CodedIndexTagFamily.TypeDefOrRef),
            reader.ReadRidIntoTable(MetadataTableName.Field),
            reader.ReadRidIntoTable(MetadataTableName.MethodDef));
}