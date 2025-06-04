namespace Reemit.Disassembler.Clr.Methods;

/// <summary>
/// Contains mutually exclusive flags indicating format of IL method header.
/// These members are complementary to <see cref="CorILMethodFlags"/> and are based on II.25.4.1 Method header type.
/// </summary>
public enum CorILMethodFormat : byte
{
    Tiny = 0x2,
    Fat = 0x3,
}