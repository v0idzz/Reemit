namespace Reemit.Disassembler.Clr.Disassembler;

public readonly record struct Operand(OperandType OperandType, byte[] OperandValue)
{
    public static readonly Operand None = new(OperandType.None, []);
}
