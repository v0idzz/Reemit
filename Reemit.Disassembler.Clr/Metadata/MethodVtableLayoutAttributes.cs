namespace Reemit.Disassembler.Clr.Metadata;

/// <summary>
/// Based on 23.1.10 Flags for methods [MethodAttributes]
/// </summary>
[Flags]
public enum MethodVtableLayoutAttributes : ushort
{
    ReuseSlot = 0x0000,
    NewSlot = 0x0100,

    Mask =
        ReuseSlot |
        NewSlot
}
