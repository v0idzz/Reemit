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
}