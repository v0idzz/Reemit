namespace Reemit.Decompiler.Clr.Disassembler;

public class Operand(OperandType operandType, IReadOnlyCollection<byte> operandValue)
{
    public static readonly Operand None = new(OperandType.None, Array.Empty<byte>());

    public OperandType OperandType { get; } = operandType;

    public IReadOnlyCollection<byte> OperandValue { get; } = operandValue;
}
