using System.Diagnostics.CodeAnalysis;

namespace Reemit.Decompiler.Clr.Disassembler;

public class OperandComparer : IEqualityComparer<Operand>
{
    public static OperandComparer Instance = new OperandComparer();

    public bool Equals(Operand? x, Operand? y) =>
        x == null && y == null ?
            true :
        x == null || y == null || x.OperandType != y.OperandType ?
            false :
            x.OperandValue.SequenceEqual(y.OperandValue);

    public int GetHashCode([DisallowNull] Operand obj)
    {
        var hc = new HashCode();
        hc.Add(obj.OperandType);
        hc.Add(obj.OperandValue.Count);

        foreach (var b in obj.OperandValue)
        {
            hc.Add(b);
        }

        return hc.ToHashCode();
    }
}
