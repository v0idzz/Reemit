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
            Opcode.beq_s => Read8(),
            Opcode.call => Read32(),
            _ => Array.Empty<byte>(),
        };

    private byte[] DecodeOperand(ExtendedOpcode extendedOpcode)
    {
        return [];
    }

    private byte[] Read8() => _binaryReader.ReadBytes(1);

    private byte[] Read16() => _binaryReader.ReadBytes(2);

    private byte[] Read32() => _binaryReader.ReadBytes(4);

    private byte[] DecodeOperand64() => _binaryReader.ReadBytes(8);
}