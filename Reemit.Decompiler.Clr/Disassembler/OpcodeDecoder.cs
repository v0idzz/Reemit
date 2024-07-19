namespace Reemit.Decompiler.Clr.Disassembler;

public class OpcodeDecoder(Stream stream) : IDecoder<OpcodeInfo>
{
    public OpcodeInfo Decode()
    {
        var opcode = (Opcode)stream.ReadByte();

        return new(
            opcode,
            opcode == Opcode.Extended ? (ExtendedOpcode)stream.ReadByte() : ExtendedOpcode.None);
    }
}