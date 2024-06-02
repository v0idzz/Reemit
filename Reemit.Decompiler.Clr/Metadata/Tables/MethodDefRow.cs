using System.Reflection;

namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class MethodDefRow(
    uint rva,
    ushort implFlags,
    ushort flags,
    uint name,
    uint signature,
    uint paramList) : IMetadataTableRow<MethodDefRow>
{
    public static MetadataTableName TableName => MetadataTableName.MethodDef;

    public uint Rva { get; } = rva;
    public ushort ImplFlags { get; } = implFlags;
    public ushort Flags { get; } = flags;
    public uint Name { get; } = name;
    public uint Signature { get; } = signature;
    public uint ParamList { get; } = paramList;

    public MethodImplCodeTypeAttributes CodeType =>
        (MethodImplCodeTypeAttributes)(ImplFlags & (ushort)MethodImplCodeTypeAttributes.Mask);

    public MethodImplManagedAttributes Managed =>
        (MethodImplManagedAttributes)(ImplFlags & (ushort)MethodImplManagedAttributes.Mask);

    public MethodImplAttributes MethodImpl =>
        (MethodImplAttributes)(ImplFlags & (ushort)MethodImplAttributes.Mask);

    public MethodMemberAccessAttributes MethodMemberAccess =>
        (MethodMemberAccessAttributes)(Flags & (ushort)MethodMemberAccessAttributes.Mask);

    public MethodVtableLayoutAttributes MethodVtableLayout =>
        (MethodVtableLayoutAttributes)(Flags & (ushort)MethodVtableLayoutAttributes.Mask);

    public MethodAttributes MethodFlags =>
        (MethodAttributes)(Flags & (ushort)MethodAttributes.Mask);

    public bool IsStatic => (MethodFlags & MethodAttributes.Static) != 0;
    public bool IsFinal => (MethodFlags & MethodAttributes.Final) != 0;
    public bool IsVirtual => (MethodFlags & MethodAttributes.Virtual) != 0;
    public bool IsHideBySig => (MethodFlags & MethodAttributes.HideBySig) != 0;
    public bool IsStrict => (MethodFlags & MethodAttributes.Strict) != 0;
    public bool IsAbstract => (MethodFlags & MethodAttributes.Abstract) != 0;
    public bool IsSpecialName => (MethodFlags & MethodAttributes.SpecialName) != 0;
    public bool IsPInvokeImpl => (MethodFlags & MethodAttributes.PInvokeImpl) != 0;
    public bool IsUnmanagedExport => (MethodFlags & MethodAttributes.UnmanagedExport) != 0;
    public bool IsRTSpecialName => (MethodFlags & MethodAttributes.RTSpecialName) != 0;
    public bool HasSecurity => (MethodFlags & MethodAttributes.HasSecurity) != 0;
    public bool RequireSecObject => (MethodFlags & MethodAttributes.RequireSecObject) != 0;

    public static MethodDefRow Read(MetadataTableDataReader reader)
    {
        var rva = reader.ReadUInt32();

        // Todo:
        // Validate these by ORing all flags, then AND NOTing to verify
        // no unexpected bits are set.
        var implFlags = reader.ReadUInt16();
        var flags = reader.ReadUInt16();

        return new(
            rva,
            implFlags,
            flags,
            reader.ReadStringRid(),
            reader.ReadBlobRid(),
            reader.ReadRidIntoTable(MetadataTableName.Param));
    }
}