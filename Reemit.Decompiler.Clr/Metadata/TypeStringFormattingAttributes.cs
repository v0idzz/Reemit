namespace Reemit.Decompiler.Clr.Metadata;

/// <summary>
/// Based on II.23.1.15 Flags for types [TypeAttributes] 
/// </summary>
public enum TypeStringFormattingAttributes : uint
{
    AnsiClass = 0x0,
    UnicodeClass = 0x10000,
    AutoClass = 0x20000,
    CustomFormatClass = 0x30000,

    /// <summary>
    /// A mask to retrieve non-standard encoding information for native interop.
    /// </summary>
    CustomStringFormatMask = 0xC00000,

    Mask =
        AnsiClass |
        UnicodeClass |
        AutoClass |
        CustomFormatClass
}