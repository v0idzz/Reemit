namespace Reemit.Disassembler.Clr.Disassembler;

public readonly struct OpcodeInfo
{
    public Opcode Opcode { get; }

    public ExtendedOpcode ExtendedOpcode { get; }

    public bool IsExtended => Opcode == Opcode.Extended;

    public bool IsPrefix => PrefixDetector.IsPrefix(ExtendedOpcode);

    public OpcodeInfo(Opcode opcode)
        : this(opcode, ExtendedOpcode.None)
    {
    }

    public OpcodeInfo(ExtendedOpcode extendedOpcode)
        : this(Opcode.Extended, extendedOpcode)
    {
    }

    public OpcodeInfo(Opcode opcode, ExtendedOpcode extendedOpcode)
    {
        Opcode = opcode;
        ExtendedOpcode = extendedOpcode;
    }

    public static implicit operator OpcodeInfo(Opcode opcode) => new(opcode);

    public static implicit operator OpcodeInfo(ExtendedOpcode extendedOpcode) => new(extendedOpcode);
}
