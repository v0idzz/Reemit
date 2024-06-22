namespace Reemit.Decompiler.Clr.Metadata.Tables;

public class MethodDefRow(
    uint rid,
    uint rva,
    ushort implFlags,
    ushort flags,
    uint name,
    uint signature,
    uint paramList) : MetadataTableRow<MethodDefRow>(rid), IMetadataTableRow<MethodDefRow>
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

    // Should these include checks to ensure MethodImpl is not MaxMethodImplVal?
    // Need to study the spec further to ensure this is handled correctly.
    public bool IsForwardRef => MethodImpl.HasFlag(MethodImplAttributes.ForwardRef);
    public bool IsPreserveSig => MethodImpl.HasFlag(MethodImplAttributes.PreserveSig);
    public bool IsInternalCall => MethodImpl.HasFlag(MethodImplAttributes.InternalCall);
    public bool IsSynchronized => MethodImpl.HasFlag(MethodImplAttributes.Synchronized);
    public bool IsNoInlining => MethodImpl.HasFlag(MethodImplAttributes.NoInlining);
    public bool IsNoOptimization => MethodImpl.HasFlag(MethodImplAttributes.NoOptimization);
    public bool IsMaxMethodImplVal => (MethodImplAttributes)ImplFlags == MethodImplAttributes.MaxMethodImplVal;

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

    public static MethodDefRow Read(uint rid, MetadataTableDataReader reader)
    {
        var rva = reader.ReadUInt32();

        var implFlags = reader.ReadUInt16();
        var invalidImplFlags = implFlags & ~FlagMasks.MethodImplAttributesMask;

        // Thinking about this further, we should not stop parsing when encountering
        // issues like these. As a general principle, we should be at least as 
        // permissive as the most permissive runtime. As seen by the violations in
        // MS assemblies, this is probably quite permissive. Leaving this as is for
        // now to avoid blocking work, but we should probably separate the validation
        // logic and implement it in a way such that it does not interrupt further
        // parsing. Decoupling the validation would also let us perform further
        // checks on that require access to other metadata tables and similar (see
        // todo below).
        if (implFlags != (ushort)MethodImplAttributes.MaxMethodImplVal && invalidImplFlags != 0x0)
        {
            throw CreateWordFlagsImageException(nameof(MethodImplAttributes), (ushort)invalidImplFlags);
        }

        var flags = reader.ReadUInt16();
        
        var row = new MethodDefRow(
            rid,
            rva,
            implFlags,
            flags,
            reader.ReadStringRid(),
            reader.ReadBlobRid(),
            reader.ReadRidIntoTable(MetadataTableName.Param));

        // From 22.26 MethodDef : 0x06, informative text entry 7.
        if (row.IsStatic && row.IsFinal)
        {
            throw CreateInvalidFlagsImageException(MethodAttributes.Static, MethodAttributes.Final);
        }
        else if (row.IsStatic && row.IsVirtual)
        {
            throw CreateInvalidFlagsImageException(MethodAttributes.Static, MethodAttributes.Virtual);
        }
        else if (row.IsStatic && row.MethodVtableLayout == MethodVtableLayoutAttributes.NewSlot)
        {
            throw CreateInvalidFlagsImageException(MethodAttributes.Static, MethodVtableLayoutAttributes.NewSlot);
        }
        else if (row.IsFinal && row.IsAbstract)
        {
            throw CreateInvalidFlagsImageException(MethodAttributes.Final, MethodAttributes.Abstract);
        }
        else if (row.IsAbstract && row.IsPInvokeImpl)
        {
            throw CreateInvalidFlagsImageException(MethodAttributes.Abstract, MethodAttributes.PInvokeImpl);
        }
        else if (row.MethodMemberAccess == MethodMemberAccessAttributes.CompilerControlled && row.IsSpecialName)
        {
            throw CreateInvalidFlagsImageException(
                MethodMemberAccessAttributes.CompilerControlled,
                MethodAttributes.SpecialName);
        }
        else if (row.MethodMemberAccess == MethodMemberAccessAttributes.CompilerControlled && row.IsRTSpecialName)
        {
            throw CreateInvalidFlagsImageException(
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

        // Todo: 
        // Further review 22.26 MethodDef informative text and implement other checks.
        // Some may not be possible in this context as they require access to other
        // metadata tables.

        return row;
    }

    private static BadImageFormatException CreateWordFlagsImageException(string flagsName, ushort flagsWord) =>
        throw new BadImageFormatException(
            $"Invalid {flagsName}: {string.Format("{0:x4}", flagsWord)}.");

    private static BadImageFormatException CreateInvalidFlagsImageException(params Enum[] flags) =>
        throw new BadImageFormatException(
            $"Invalid flags: {string.Join(", ", flags.Select(x => x.ToString()))}.");
}