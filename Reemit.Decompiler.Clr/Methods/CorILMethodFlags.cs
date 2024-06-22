namespace Reemit.Decompiler.Clr.Methods;

/// <summary>
/// Based on II.25.4.4 Flags for method headers
/// </summary>
[Flags]
public enum CorILMethodFlags : byte
{
    FatFormat = 0x3,
    TinyFormat = 0x2,
    MoreSects = 0x8,
    InitLocals = 0x10,
}