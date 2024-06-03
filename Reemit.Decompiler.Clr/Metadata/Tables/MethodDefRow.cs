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

    public bool IsForwardRef => MethodImpl.HasFlag(MethodImplAttributes.ForwardRef);
    public bool IsPreserveSig => MethodImpl.HasFlag(MethodImplAttributes.PreserveSig);
    public bool IsInternalCall => MethodImpl.HasFlag(MethodImplAttributes.InternalCall);
    public bool IsSynchronized => MethodImpl.HasFlag(MethodImplAttributes.Synchronized);
    public bool IsNoInlining => MethodImpl.HasFlag(MethodImplAttributes.NoInlining);
    public bool IsNoOptimization => MethodImpl.HasFlag(MethodImplAttributes.NoOptimization);
    public bool IsMaxMethodImplVal => MethodImpl == MethodImplAttributes.MaxMethodImplVal;

    public bool IsStatic => MethodFlags.HasFlag(MethodAttributes.Static);
    public bool IsFinal => MethodFlags.HasFlag(MethodAttributes.Final);
    public bool IsVirtual => MethodFlags.HasFlag(MethodAttributes.Virtual);
    public bool IsHideBySig => MethodFlags.HasFlag(MethodAttributes.HideBySig);
    public bool IsStrict => MethodFlags.HasFlag(MethodAttributes.Strict);
    public bool IsAbstract => MethodFlags.HasFlag(MethodAttributes.Abstract);
    public bool IsSpecialName => MethodFlags.HasFlag(MethodAttributes.SpecialName);
    public bool IsPInvokeImpl => MethodFlags.HasFlag(MethodAttributes.PInvokeImpl);
    public bool IsUnmanagedExport => MethodFlags.HasFlag(MethodAttributes.UnmanagedExport);
    public bool IsRTSpecialName => MethodFlags.HasFlag(MethodAttributes.RTSpecialName);
    public bool HasSecurity => MethodFlags.HasFlag(MethodAttributes.HasSecurity);
    public bool RequireSecObject => MethodFlags.HasFlag(MethodAttributes.RequireSecObject);

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

        var row = new MethodDefRow(
            rva,
            implFlags,
            flags,
            reader.ReadStringRid(),
            reader.ReadBlobRid(),
            reader.ReadRidIntoTable(MetadataTableName.Param));

        // From 22.26 MethodDef : 0x06, informative text entry 7.
        if (row.IsStatic && row.IsFinal)
        {
            ThrowInvalidFlagsImageException(MethodAttributes.Static, MethodAttributes.Final);
        }
        else if (row.IsStatic && row.IsVirtual)
        {
            ThrowInvalidFlagsImageException(MethodAttributes.Static, MethodAttributes.Virtual);
        }
        else if (row.IsStatic && row.MethodVtableLayout == MethodVtableLayoutAttributes.NewSlot)
        {
            ThrowInvalidFlagsImageException(MethodAttributes.Static, MethodVtableLayoutAttributes.NewSlot);
        }
        else if (row.IsFinal && row.IsAbstract)
        {
            ThrowInvalidFlagsImageException(MethodAttributes.Final, MethodAttributes.Abstract);
        }
        else if (row.IsAbstract && row.IsPInvokeImpl)
        {
            ThrowInvalidFlagsImageException(MethodAttributes.Abstract, MethodAttributes.PInvokeImpl);
        }
        else if (row.MethodMemberAccess == MethodMemberAccessAttributes.CompilerControlled && row.IsSpecialName)
        {
            ThrowInvalidFlagsImageException(
                MethodMemberAccessAttributes.CompilerControlled,
                MethodAttributes.SpecialName);
        }
        else if (row.MethodMemberAccess == MethodMemberAccessAttributes.CompilerControlled && row.IsRTSpecialName)
        {
            ThrowInvalidFlagsImageException(
                MethodMemberAccessAttributes.CompilerControlled,
                MethodAttributes.RTSpecialName);
        }
        // From 22.26 MethodDef : 0x06, informative text entry 8.
        else if (row.IsAbstract && !row.IsVirtual)
        {
            throw new BadImageFormatException("Abstract methods must be virtual.");
        }
        // From 22.26 MethodDef : 0x06, informative text entry 9.
        else if (row.IsRTSpecialName && !row.IsSpecialName)
        {
            throw new BadImageFormatException("SpecialName is required when RTSpecialName is set.");
        }

        return row;
    }

    [DoesNotReturn]
    private static void ThrowWordFlagsImageException(string flagsName, ushort flagsWord) =>
        throw new BadImageFormatException(
                $"Invalid {flagsName}: {string.Format("{0:x4}", flagsWord)}.");

    [DoesNotReturn]
    private static void ThrowInvalidFlagsImageException(params Enum[] flags) =>
        throw new BadImageFormatException(
                $"Invalid flags: {string.Join(", ", flags.Select(x => x.ToString()))}.");
}