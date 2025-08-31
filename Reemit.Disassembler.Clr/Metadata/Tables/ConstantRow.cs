using Reemit.Disassembler.Clr.Signatures;

namespace Reemit.Disassembler.Clr.Metadata.Tables;

public class ConstantRow(uint rid, ElementType type, CodedIndex parent, uint value)
    : MetadataRecord(rid), IMetadataTableRow<ConstantRow>
{
    public static MetadataTableName TableName => MetadataTableName.Constant;

    public CodedIndex Parent => parent;
    
    public ElementType Type => type;
    
    public uint Value => value;

    public static ConstantRow Read(uint rid, MetadataTableDataReader reader)
    {
        var type = (ElementType)reader.ReadByte();
        reader.ReadByte(); // padding
        var parent = reader.ReadCodedRid(CodedIndexTagFamily.HasConstant);
        var value = reader.ReadBlobRid();
        
        return new ConstantRow(rid, type, parent, value);
    }
}