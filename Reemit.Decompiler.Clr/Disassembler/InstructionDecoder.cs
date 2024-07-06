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

    private byte[] DecodeOperand(OpcodeInfo opcodeInfo) =>
        opcodeInfo.IsExtended ?
            DecodeOperand(opcodeInfo.ExtendedOpcode) :
            DecodeOperand(opcodeInfo.Opcode);

    private byte[] DecodeOperand(Opcode opcode) =>
        opcode switch
        {
            Opcode.call => DecodeOperand32(),
            _ => Array.Empty<byte>(),
        };

    private byte[] DecodeOperand(ExtendedOpcode extendedOpcode)
    {
        return [];
    }

    private byte[] DecodeOperand32() => _binaryReader.ReadBytes(4);
}