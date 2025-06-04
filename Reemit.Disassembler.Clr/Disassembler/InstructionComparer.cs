namespace Reemit.Disassembler.Clr.Disassembler;

public class InstructionComparer : IEqualityComparer<Instruction>
{
    public static readonly InstructionComparer Instance = new InstructionComparer();

    public bool Equals(Instruction x, Instruction y) =>
        OpcodeInfoComparer.Instance.Equals(x.OpcodeInfo, y.OpcodeInfo) &&
        OperandComparer.Instance.Equals(x.Operand, y.Operand);

    public int GetHashCode(Instruction obj)
    {
        var hc = new HashCode();
        hc.Add(obj.Operand);
        hc.Add(obj.OpcodeInfo);

        return hc.ToHashCode();
    }
}
