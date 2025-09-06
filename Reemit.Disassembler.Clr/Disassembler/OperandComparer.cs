namespace Reemit.Disassembler.Clr.Disassembler;

public class OperandComparer : IEqualityComparer<Operand>
{
    public static readonly OperandComparer Instance = new OperandComparer();

    public bool Equals(Operand x, Operand y) =>
        x.OperandType == y.OperandType &&
        x.OperandValue.SequenceEqual(y.OperandValue);

    public int GetHashCode(Operand obj)
    {
        var hc = new HashCode();
        hc.Add(obj.OperandType);
        hc.Add(obj.OperandValue.Length);

        foreach (var b in obj.OperandValue)
        {
            hc.Add(b);
        }

        return hc.ToHashCode();
    }
}
