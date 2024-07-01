namespace Reemit.Decompiler.Clr.Methods;

/// <summary>
/// Based on method data section flags defined in II.25.4.5 Method data section
/// </summary>
[Flags]
public enum CorILMethodSectionFlags : byte
{
    EHTable = 0x1,
    OptILTable = 0x2,
    FatFormat = 0x40,
    MoreSects = 0x80
}