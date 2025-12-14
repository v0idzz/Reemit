namespace Reemit.Disassembler.Clr.Disassembler;

public class InstructionDecoder : IDecoder<Instruction>
{
    private readonly BinaryReader _binaryReader;

    private readonly OpcodeDecoder _opcodeDecoder;

    private readonly long _initialPos;

    public InstructionDecoder(Stream stream)
    {
        _binaryReader = new BinaryReader(stream);
        _opcodeDecoder = new OpcodeDecoder(stream);
        _initialPos = stream.Position;
    }

    public Instruction Decode()
    {
        var offset = _binaryReader.BaseStream.Position - _initialPos;
        var opcode = _opcodeDecoder.Decode();
        var operand = DecodeOperand(opcode);

        return new Instruction((uint)offset, opcode, operand);
    }

    private Operand DecodeOperand(OpcodeInfo opcodeInfo)
    {
        var operandType = OpcodeOperandTable.GetOperandType(opcodeInfo);
        byte[] operandValue;

        if (operandType != OperandType.JumpTable)
        {
            var operandSize = OperandSizeTable.SizeTable[operandType];
            operandValue = _binaryReader.ReadBytes(operandSize);
        }
        else
        {
            operandValue = DecodeJumpTable();
        }

        return new(operandType, operandValue);
    }

    private byte[] DecodeJumpTable()
    {
        var jumpCount = _binaryReader.ReadUInt32();

        // Technically incorrect given jumpCount is an unsigned int32.
        var jumps = _binaryReader.ReadBytes((int)(jumpCount * 4));

        // Somewhat inefficient, but doing it this way for now.
        return BitConverter.GetBytes(jumpCount).Concat(jumps).ToArray();
    }
}