namespace Reemit.Disassembler.Clr.Methods;

/// <summary>
/// Based on II.25.4.6 Exception handling clauses
/// </summary>
[Flags]
public enum CorILExceptionClauses : ushort
{
    Exception = 0x0,
    Filter = 0x1,
    Finally = 0x2,
    Fault = 0x4
}