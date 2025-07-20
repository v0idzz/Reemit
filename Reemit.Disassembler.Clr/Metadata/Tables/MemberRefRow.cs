namespace Reemit.Disassembler.Clr.Metadata.Tables;

public class MemberRefRow(uint rid, CodedIndex @class, uint name, uint signature)
    : MetadataRecord(rid), IMetadataTableRow<MemberRefRow>
{
    public static MetadataTableName TableName => MetadataTableName.MemberRef;

    public uint Rid => rid;
    
    public CodedIndex Class => @class;
    
    public uint Name => name;
    
    public uint Signature => signature;

    public static MemberRefRow Read(uint rid, MetadataTableDataReader reader)
    {
        var @class = reader.ReadCodedRid(CodedIndexTagFamily.MemberRefParent);
        var name = reader.ReadStringRid();
        var signature = reader.ReadBlobRid();

        return new MemberRefRow(rid, @class, name, signature);
    }
}