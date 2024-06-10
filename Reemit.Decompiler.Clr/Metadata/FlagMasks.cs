namespace Reemit.Decompiler.Clr.Metadata;

public static class FlagMasks
{
    /// <summary>
    /// A mask intended for validation of TypeAttributes bitfields.
    /// </summary>
    public const uint TypeAttributesMask = 
        (uint)TypeAttributes.Mask |
        (uint)TypeClassLayoutAttributes.Mask |
        (uint)TypeClassSemanticsAttributes.Mask |
        (uint)TypeImplementationAttributes.Mask |
        (uint)TypeStringFormattingAttributes.Mask |
        (uint)TypeVisibilityAttributes.Mask;
    
    public const uint FieldAccessMask = 0x7;

    /// <summary>
    /// A mask intended for validation of MethodImplAttributes bitfields. Excludes
    /// MaxMethodImplVal as it sets all bits in the word.
    /// </summary>
    public const ushort MethodImplAttributesMask =
        (ushort)MethodImplCodeTypeAttributes.Mask |
        (ushort)MethodImplManagedAttributes.Mask |
        (ushort)MethodImplAttributes.Mask;

    /// <summary>
    /// A mask intended for validation of MethodAttributes bitfields.
    /// </summary>
    public const ushort MethodAttributesMask =
        (ushort)MethodMemberAccessAttributes.Mask |
        (ushort)MethodVtableLayoutAttributes.Mask |
        (ushort)MethodAttributes.Mask;
}