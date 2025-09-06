using System.Text;
using Reemit.Common;
using Reemit.Disassembler.Clr.Disassembler;
using Reemit.Disassembler.Clr.Metadata;
using Reemit.Disassembler.Clr.Metadata.Streams;

namespace Reemit.Disassembler.Clr.UnitTests.Disassembler;

public class OperandEmitterTests
{
    [Fact]
    public void Emit_Int8_AppendsSignedByte()
    {
        // Arrange
        var operand = new Operand(OperandType.Int8, [unchecked((byte)-42)]);
        var emitter = new OperandEmitter(CreateEmptyUserStringsHeap());

        // Act
        var result = EmitToString(operand, emitter);

        // Assert
        Assert.Equal("-42", result);
    }

    [Fact]
    public void Emit_Int16_AppendsShort()
    {
        // Arrange
        var bytes = BitConverter.GetBytes((short)-12345);
        var operand = new Operand(OperandType.Int16, bytes);
        var emitter = new OperandEmitter(CreateEmptyUserStringsHeap());

        // Act
        var result = EmitToString(operand, emitter);

        // Assert
        Assert.Equal("-12345", result);
    }

    [Fact]
    public void Emit_Int32_AppendsInt()
    {
        // Arrange
        var bytes = BitConverter.GetBytes(-987654321);
        var operand = new Operand(OperandType.Int32, bytes);
        var emitter = new OperandEmitter(CreateEmptyUserStringsHeap());

        // Act
        var result = EmitToString(operand, emitter);

        // Assert
        Assert.Equal("-987654321", result);
    }

    [Fact]
    public void Emit_Int64_AppendsLong()
    {
        // Arrange
        var bytes = BitConverter.GetBytes(long.MinValue);
        var operand = new Operand(OperandType.Int64, bytes);
        var emitter = new OperandEmitter(CreateEmptyUserStringsHeap());

        // Act
        var result = EmitToString(operand, emitter);

        // Assert
        Assert.Equal(long.MinValue.ToString(), result);
    }

    [Fact]
    public void Emit_UInt8_AppendsByte()
    {
        // Arrange
        var operand = new Operand(OperandType.UInt8, [255]);
        var emitter = new OperandEmitter(CreateEmptyUserStringsHeap());

        // Act
        var result = EmitToString(operand, emitter);

        // Assert
        Assert.Equal("255", result);
    }

    [Fact]
    public void Emit_UInt16_AppendsUnsignedShort()
    {
        // Arrange
        var bytes = BitConverter.GetBytes((ushort)65535);
        var operand = new Operand(OperandType.UInt16, bytes);
        var emitter = new OperandEmitter(CreateEmptyUserStringsHeap());

        // Act
        var result = EmitToString(operand, emitter);

        // Assert
        Assert.Equal("65535", result);
    }

    [Fact]
    public void Emit_UInt32_AppendsUnsignedInt()
    {
        // Arrange
        var bytes = BitConverter.GetBytes(uint.MaxValue);
        var operand = new Operand(OperandType.UInt32, bytes);
        var emitter = new OperandEmitter(CreateEmptyUserStringsHeap());

        // Act
        var result = EmitToString(operand, emitter);

        // Assert
        Assert.Equal(uint.MaxValue.ToString(), result);
    }

    [Fact]
    public void Emit_UInt64_AppendsUnsignedLong()
    {
        // Arrange
        var bytes = BitConverter.GetBytes(ulong.MaxValue);
        var operand = new Operand(OperandType.UInt64, bytes);
        var emitter = new OperandEmitter(CreateEmptyUserStringsHeap());
        
        // Act
        var result = EmitToString(operand, emitter);

        // Assert
        Assert.Equal(ulong.MaxValue.ToString(), result);
    }

    [Fact]
    public void Emit_Float32_AppendsFloatInvariantCulture()
    {
        // Arrange
        var bytes = BitConverter.GetBytes(123.456f);
        var operand = new Operand(OperandType.Float32, bytes);
        var emitter = new OperandEmitter(CreateEmptyUserStringsHeap());
        
        // Act
        var result = EmitToString(operand, emitter);

        // Assert
        Assert.Equal("123.456", result);
    }

    [Fact]
    public void Emit_Float64_AppendsDoubleInvariantCulture()
    {
        // Arrange
        var bytes = BitConverter.GetBytes(9876.54321);
        var operand = new Operand(OperandType.Float64, bytes);
        var emitter = new OperandEmitter(CreateEmptyUserStringsHeap());
        
        // Act
        var result = EmitToString(operand, emitter);

        // Assert
        Assert.Equal("9876.54321", result);
    }

    [Fact]
    public void Emit_MetadataToken_NonUserString_AppendsToken()
    {
        // Arrange
        // MemberRef, Rid: 16
        byte[] metadataTokenBytes = [0x10, 0x0, 0x0, 0x0A];
        var operand = new Operand(OperandType.MetadataToken, metadataTokenBytes);
        var emitter = new OperandEmitter(CreateEmptyUserStringsHeap());
        
        // Act
        var result = EmitToString(operand, emitter);
        
        // Assert
        Assert.Equal("MetadataToken { TableRef: MemberRef, Index: 16 }", result);
    }

    [Fact]
    public async Task Emit_MetadataToken_UserString_AppendsStringFromHeap()
    {
        // Arrange
        byte[] metadataTokenBytes = [0x29, 0x0, 0x0, 0x70];
        await using var fileStream = File.OpenRead("Resources/userstringsheapstream.bin");
        using var reader = new BinaryReader(fileStream);
        using var sharedReader = new SharedReader(0, reader);
        
        var operand = new Operand(OperandType.MetadataToken, metadataTokenBytes);
        
        var emitter = new OperandEmitter(new UserStringsHeapStream(sharedReader,
            new StreamHeader(0, (uint)fileStream.Length, UserStringsHeapStream.Name)));
        
        // Act
        var result = EmitToString(operand, emitter);
        
        Assert.Equal("\"here goes some string\"", result);
    }

    [Fact]
    public void Emit_UnknownOperandType_AppendsHexDump()
    {
        // Arrange
        var operand = new Operand((OperandType)999, [0xDE, 0xAD, 0xBE, 0xEF]);
        var emitter = new OperandEmitter(CreateEmptyUserStringsHeap());
        
        // Act
        var result = EmitToString(operand, emitter);

        // Assert
        Assert.Equal("999 {de ad be ef}", result);
    }

    private static UserStringsHeapStream CreateEmptyUserStringsHeap()
    {
        byte[] bytes = [];
        var ms = new MemoryStream(bytes);
        var reader = new SharedReader(0, new BinaryReader(ms));
        var header = new StreamHeader(0, 0, UserStringsHeapStream.Name);
        return new UserStringsHeapStream(reader, header);
    }
    
    private static string EmitToString(Operand operand, OperandEmitter sut)
    {
        var sb = new StringBuilder();
        sut.Emit(operand, sb);
        return sb.ToString();
    }
}