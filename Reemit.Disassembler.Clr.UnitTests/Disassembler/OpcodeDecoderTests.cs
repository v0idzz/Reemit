using Reemit.Disassembler.Clr.Disassembler;

namespace Reemit.Disassembler.Clr.UnitTests.Disassembler;

public class OpcodeDecoderTests
{
    [Theory]
    [InlineData(Opcode.nop, 0x00)]
    [InlineData(Opcode.add, 0x58)]
    [InlineData(Opcode.conv_u, 0xe0)]
    public void DecodeOpcode_Standard_OpcodeDecoded(Opcode expectedOpcode, byte opcodeByte)
    {
        // Arrange

        // Act
        var actualOpcode = DecodeBuffer([ opcodeByte ]);

        // Assert
        Assert.Equal(expectedOpcode, actualOpcode.Opcode);
        Assert.False(actualOpcode.IsExtended);
        Assert.Equal(ExtendedOpcode.None, actualOpcode.ExtendedOpcode);
    }

    [Theory]
    [InlineData(ExtendedOpcode.arglist, false, 0x00)]
    [InlineData(ExtendedOpcode.ldarga, false, 0x0a)]
    [InlineData(ExtendedOpcode.@readonly, true, 0x1e)]
    public void DecodeOpcode_Extended_OpcodeDecoded(
        ExtendedOpcode expectedExtendedOpcode,
        bool expectedPrefix,
        byte opcodeByte)
    {
        // Arrange

        // Act
        var actualOpcode = DecodeBuffer([ 0xfe, opcodeByte ]);

        // Assert
        Assert.Equal(Opcode.Extended, actualOpcode.Opcode);
        Assert.True(actualOpcode.IsExtended);
        Assert.Equal(expectedExtendedOpcode, actualOpcode.ExtendedOpcode);
        Assert.Equal(expectedPrefix, actualOpcode.IsPrefix);
    }

    private OpcodeInfo DecodeBuffer(byte[] buffer)
    {
        using var stream = new MemoryStream(buffer);
        var decoder = new OpcodeDecoder(stream);
        
        return decoder.Decode();
    }
}
