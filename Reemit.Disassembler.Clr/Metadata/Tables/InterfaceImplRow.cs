namespace Reemit.Disassembler.Clr.Metadata.Tables;

public class InterfaceImplRow(uint rid, uint @class, CodedIndex @interface)
    : MetadataRecord(rid), IMetadataTableRow<InterfaceImplRow>
{
    public static MetadataTableName TableName => MetadataTableName.InterfaceImpl;

    public uint Rid { get; } = rid;

    public uint Class { get; } = @class;

    public CodedIndex Interface { get; } = @interface;

    public static InterfaceImplRow Read(uint rid, MetadataTableDataReader reader)
    {
        var @class = reader.ReadRidIntoTable(MetadataTableName.TypeDef);
        var @interface = reader.ReadCodedRid(CodedIndexTagFamily.TypeDefOrRef);

        return new InterfaceImplRow(rid, @class, @interface);
    }
}