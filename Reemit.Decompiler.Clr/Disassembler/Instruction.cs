namespace Reemit.Decompiler.Clr.Disassembler;

public class Instruction
{
    public OpcodeInfo OpcodeInfo { get; }

    public Operand Operand { get; }

    public Instruction(OpcodeInfo opcodeInfo)
        : this(opcodeInfo, Operand.None)
    {

    }

    public Instruction(OpcodeInfo opcodeInfo, Operand operand)
    {
        OpcodeInfo = opcodeInfo;
        Operand = operand;
    }
}
