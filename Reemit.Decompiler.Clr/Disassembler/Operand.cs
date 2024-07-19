namespace Reemit.Decompiler.Clr.Disassembler;

public readonly record struct Operand(OperandType OperandType, IReadOnlyCollection<byte> OperandValue)
{
    public static readonly Operand None = new(OperandType.None, Array.Empty<byte>());
}
