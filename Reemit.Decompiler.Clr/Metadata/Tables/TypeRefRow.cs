using Reemit.Common;

namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class TypeRefRow(
    RangeMapped<CodedIndex> resolutionScope,
    RangeMapped<uint> typeName,
    RangeMapped<uint> typeNamespace)
    : IMetadataTableRow<TypeRefRow>
{
    public static MetadataTableName TableName => MetadataTableName.TypeRef;

    public RangeMapped<CodedIndex> ResolutionScope { get; } = resolutionScope;
    public RangeMapped<uint> TypeName { get; } = typeName;
    public RangeMapped<uint> TypeNamespace { get; } = typeNamespace;

    public static TypeRefRow Read(MetadataTableDataReader reader) =>
        new(
            reader.ReadMappedCodedRid(CodedIndexTagFamily.ResolutionScope),
            reader.ReadMappedStringRid(),
            reader.ReadMappedStringRid());
}