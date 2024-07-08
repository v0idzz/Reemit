using System.Diagnostics.CodeAnalysis;

namespace Reemit.Decompiler.Clr.Disassembler;

public class OpcodeInfoComparer : IEqualityComparer<OpcodeInfo>
{
    public static OpcodeInfoComparer Instance = new OpcodeInfoComparer();

    public bool Equals(OpcodeInfo? x, OpcodeInfo? y) =>
        x?.Opcode == y?.Opcode && x?.ExtendedOpcode == y?.ExtendedOpcode;

    public int GetHashCode([DisallowNull] OpcodeInfo obj)
    {
        var hc = new HashCode();
        hc.Add(obj.Opcode);
        hc.Add(obj.ExtendedOpcode);

        return hc.ToHashCode();
    }
}
