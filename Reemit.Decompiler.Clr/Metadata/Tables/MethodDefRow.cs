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
        var codeType = (MethodImplAttributes)(implFlagsBits & (ushort)MethodImplAttributes.CodeTypeMask);
        
        var managed = (MethodImplAttributes)(implFlagsBits & (ushort)MethodImplAttributes.ManagedMask);
        var isManaged = managed == MethodImplAttributes.Managed;
        var isForwardRef = (implFlagsBits & (ushort)MethodImplAttributes.ForwardRef) != 0;
        var isPreserveSig = (implFlagsBits & (ushort)MethodImplAttributes.PreserveSig) != 0;
        var isInternalCall = (implFlagsBits & (ushort)MethodImplAttributes.InternalCall) != 0;
        var isSynchronized = (implFlagsBits & (ushort)MethodImplAttributes.Synchronized) != 0;
        var isNoInlining = (implFlagsBits & (ushort)MethodImplAttributes.NoInlining) != 0;
        var isMaxMethodImplVal = (implFlagsBits & (ushort)MethodImplAttributes.MaxMethodImplVal) != 0;
        var isNoOptimization = (implFlagsBits & (ushort)MethodImplAttributes.NoOptimization) != 0;

        var flagsBits = reader.ReadUInt16();
        var methodAccess = (MethodAttributes)(flagsBits & (ushort)MethodAttributes.MemberAccessMask);
        var vtableLayout = (MethodAttributes)(flagsBits & (ushort)MethodAttributes.VtableLayoutMask);

        var isStatic = (flagsBits & (int)MethodAttributes.Static) != 0;
        var isFinal = (flagsBits & (int)MethodAttributes.Final) != 0;
        var isVirtual = (flagsBits & (int)MethodAttributes.Virtual) != 0;
        var isHideBySig = (flagsBits & (int)MethodAttributes.HideBySig) != 0;


        var name = reader.ReadStringRid();
        var signature = reader.ReadBlobRid();
        var paramList = reader.ReadRidIntoTable(MetadataTableName.Param);

        return null!;
    }
}