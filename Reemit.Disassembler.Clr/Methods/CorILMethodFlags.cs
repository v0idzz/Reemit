namespace Reemit.Disassembler.Clr.Methods;

/// <summary>
/// Based on II.25.4.4 Flags for method headers
/// </summary>
[Flags]
public enum CorILMethodFlags : byte
{
    MoreSects = 0x8,
    InitLocals = 0x10,
    
    /// <summary>
    /// A mask to be used to extract <see cref="CorILMethodFormat"/> value.
    /// </summary>
    FormatMask = 0x3
}