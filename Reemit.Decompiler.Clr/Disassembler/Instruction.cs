namespace Reemit.Decompiler.Clr.Disassembler;

public class Instruction
{
    public OpcodeInfo OpcodeInfo { get; }

    public IReadOnlyCollection<byte> Operand { get; }

    public Instruction(OpcodeInfo opcodeInfo)
        : this(opcodeInfo, Array.Empty<byte>())
    {

    }

    public Instruction(
        OpcodeInfo opcodeInfo,
        IReadOnlyCollection<byte> operand)
    {
        OpcodeInfo = opcodeInfo;
        Operand = operand;
    }
}
