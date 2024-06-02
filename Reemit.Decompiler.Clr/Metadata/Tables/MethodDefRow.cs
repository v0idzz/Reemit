using System.Reflection;

namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class MethodDefRow(
    uint rva,
    MethodImplAttributes implFlags,
    MethodAttributes flags,
    uint name,
    uint signature,
    uint paramList) : IMetadataTableRow<MethodDefRow>
{
    public static MetadataTableName TableName => MetadataTableName.MethodDef;

    public uint Rva { get; } = rva;
    public MethodImplAttributes ImplFlags { get; } = implFlags;
    public MethodAttributes Flags { get; } = flags;
    public uint Name { get; } = name;
    public uint Signature { get; } = signature;
    public uint ParamList { get; } = paramList;

    public static MethodDefRow Read(MetadataTableDataReader reader)
    {
        var rva = reader.ReadUInt32();
        
        var implFlagsBits = reader.ReadUInt16();
        var codeType = (MethodImplCodeTypeAttributes)(implFlagsBits & (ushort)MethodImplCodeTypeAttributes.Mask);
        var managed = (MethodImplManagedAttributes)(implFlagsBits & (ushort)MethodImplManagedAttributes.Mask);
        var methodImpl = (MethodImplManagedAttributes)(implFlagsBits & (ushort)MethodImplAttributes.Mask);

        var flagsBits = reader.ReadUInt16();
        var methodAccess = (MethodMemberAccessAttributes)(flagsBits & (ushort)MethodMemberAccessAttributes.Mask);
        var vtableLayout = (MethodVtableLayoutAttributes)(flagsBits & (ushort)MethodVtableLayoutAttributes.Mask);
        var memberAttributes = (MethodAttributes)(flagsBits & (ushort)MethodAttributes.Mask);


        var name = reader.ReadStringRid();
        var signature = reader.ReadBlobRid();
        var paramList = reader.ReadRidIntoTable(MetadataTableName.Param);

        return null!;
    }
}