namespace Reemit.Disassembler.Clr.Metadata;

/// <summary>
/// Based on II.23.1.13 Flags for params [ParamAttributes]
/// </summary>
[Flags]
public enum ParamAttributes : ushort
{
    In = 0x0001,
    Out = 0x0002,
    Optional = 0x0010,
    HasDefault = 0x1000,
    HasFieldMarshal = 0x2000,
    Unused = 0xcfe0,
}