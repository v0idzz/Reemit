namespace Reemit.Decompiler.Clr.Disassembler;

public record Operand(OperandType OperandType, IReadOnlyCollection<byte> OperandValue)
{
    public static readonly Operand None = new(OperandType.None, Array.Empty<byte>());
}
