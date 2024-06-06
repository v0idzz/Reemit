using Reemit.Common;

namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class TypeDefRow(
    RangeMapped<TypeAttributes> flags,
    RangeMapped<uint> typeName,
    RangeMapped<uint> typeNamespace,
    RangeMapped<CodedIndex> extends,
    RangeMapped<uint> fieldList,
    RangeMapped<uint> methodList)
    : IMetadataTableRow<TypeDefRow>
{
    public static MetadataTableName TableName => MetadataTableName.TypeDef;

    public RangeMapped<TypeAttributes> Flags { get; } = flags;
    public RangeMapped<uint> TypeName { get; } = typeName;
    public RangeMapped<uint> TypeNamespace { get; } = typeNamespace;
    public RangeMapped<CodedIndex> Extends { get; } = extends;
    public RangeMapped<uint> FieldList { get; } = fieldList;
    public RangeMapped<uint> MethodList { get; } = methodList;

    public static TypeDefRow Read(MetadataTableDataReader reader) =>
        new(
            reader
                .ReadMappedUInt32()
                .Select(x => (TypeAttributes)(x & FlagMasks.TypeAttributesMask)),
            reader.ReadMappedStringRid(),
            reader.ReadMappedStringRid(),
            reader.ReadMappedCodedRid(CodedIndexTagFamily.TypeDefOrRef),
            reader.ReadMappedRidIntoTable(MetadataTableName.Field),
            reader.ReadMappedRidIntoTable(MetadataTableName.MethodDef));
}