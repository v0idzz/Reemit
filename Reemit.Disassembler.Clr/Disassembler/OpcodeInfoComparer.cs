﻿namespace Reemit.Disassembler.Clr.Disassembler;

public class OpcodeInfoComparer : IEqualityComparer<OpcodeInfo>
{
    public static readonly OpcodeInfoComparer Instance = new OpcodeInfoComparer();

    public bool Equals(OpcodeInfo x, OpcodeInfo y) =>
        x.Opcode == y.Opcode && x.ExtendedOpcode == y.ExtendedOpcode;

    public int GetHashCode(OpcodeInfo obj)
    {
        var hc = new HashCode();
        hc.Add(obj.Opcode);
        hc.Add(obj.ExtendedOpcode);

        return hc.ToHashCode();
    }
}
