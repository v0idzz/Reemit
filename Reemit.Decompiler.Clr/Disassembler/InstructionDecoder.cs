using System.Collections.Immutable;

namespace Reemit.Decompiler.Clr.Disassembler;

public class InstructionDecoder(Stream stream) : IDecoder<Instruction>
{
    private readonly BinaryReader _binaryReader = new(stream);

    private readonly OpcodeDecoder _opcodeDecoder = new(stream);

    public Instruction Decode()
    {
        var opcode = _opcodeDecoder.Decode();
        var operand = DecodeOperand(opcode);

        return new Instruction(opcode, operand);
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