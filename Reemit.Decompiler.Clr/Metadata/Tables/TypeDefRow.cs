namespace Reemit.Decompiler.Cli.Metadata.Tables;

public class TypeDefRow : IMetadataTableRow
{
    public TypeAttributes Flags { get; private set; }
    public uint TypeName { get; private set; }
    public uint TypeNamespace { get; private set; }
    public CodedIndex Extends { get; private set; } = null!;
    public uint FieldList { get; private set; }
    public uint MethodList { get; private set; }

    public void Read(MetadataTableDataReader reader)
    {
        Flags = (TypeAttributes)(reader.ReadUInt32() & FlagMasks.TypeAttributesMask);
        TypeName = reader.ReadStringRid();
        TypeNamespace = reader.ReadStringRid();
        Extends = reader.ReadCodedRid(CodedIndexTagFamily.TypeDefOrRef);
        FieldList = reader.ReadRidIntoTable(MetadataTableName.Field);
        MethodList = reader.ReadRidIntoTable(MetadataTableName.MethodDef);
    }
}