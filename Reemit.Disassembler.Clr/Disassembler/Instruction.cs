namespace Reemit.Disassembler.Clr.Disassembler;

public readonly struct Instruction
{
    public uint Offset { get; }

    public OpcodeInfo OpcodeInfo { get; }

    public Operand Operand { get; }

    public Instruction(uint offset, OpcodeInfo opcodeInfo)
        : this(offset, opcodeInfo, Operand.None)
    {
    }

    public Instruction(uint offset, OpcodeInfo opcodeInfo, Operand operand)
    {
        Offset = offset;
        OpcodeInfo = opcodeInfo;
        Operand = operand;
    }
}
