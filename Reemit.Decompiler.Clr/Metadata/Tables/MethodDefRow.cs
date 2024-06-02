using System.Diagnostics.CodeAnalysis;

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

    public bool IsForwardRef => (MethodImpl & MethodImplAttributes.ForwardRef) != 0;
    public bool IsPreserveSig => (MethodImpl & MethodImplAttributes.PreserveSig) != 0;
    public bool IsInternalCall => (MethodImpl & MethodImplAttributes.InternalCall) != 0;
    public bool IsSynchronized => (MethodImpl & MethodImplAttributes.Synchronized) != 0;
    public bool IsNoInlining => (MethodImpl & MethodImplAttributes.NoInlining) != 0;
    public bool IsNoOptimization => (MethodImpl & MethodImplAttributes.NoOptimization) != 0;
    public bool IsMaxMethodImplVal => MethodImpl == MethodImplAttributes.MaxMethodImplVal;

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

        var implFlags = reader.ReadUInt16();
        var invalidImplFlags = implFlags & ~FlagMasks.MethodImplAttributesMask;

        if (implFlags != (ushort)MethodImplAttributes.MaxMethodImplVal && invalidImplFlags != 0x0)
        {
            ThrowWordFlagsImageException("MethodImplAttributes", (ushort)invalidImplFlags);
        }

        var flags = reader.ReadUInt16();
        var invalidFlags = flags & ~FlagMasks.MethodAttributesMask;

        if (invalidFlags != 0x0)
        {
            ThrowWordFlagsImageException("MethodAttributes", (ushort)invalidFlags);
        }

        return new(
            rva,
            implFlags,
            flags,
            reader.ReadStringRid(),
            reader.ReadBlobRid(),
            reader.ReadRidIntoTable(MetadataTableName.Param));
    }

    [DoesNotReturn]
    private static void ThrowWordFlagsImageException(string flagsName, ushort flagsWord) =>
        throw new BadImageFormatException(
                $"Invalid {flagsName}: {string.Format("{0:x4}", flagsWord)}.");
}