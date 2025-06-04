namespace Reemit.Disassembler.Clr.Metadata;

public enum FieldAttributes : uint
{
    CompilerControlled = 0x0,
    Private = 0x1,
    FamAndAssem = 0x2,
    Assembly = 0x3,
    Family = 0x4,
    FamOrAssem = 0x5,
    Public = 0x6,
    Static = 0x10,
    InitOnly = 0x20,
    Literal = 0x40,
    NotSerialized = 0x80,
    SpecialName = 0x200
}