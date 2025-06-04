using Reemit.Common;
using Reemit.Disassembler.Clr.Metadata.Streams;

namespace Reemit.Disassembler.Clr.UnitTests.Metadata.Streams;

public class SignatureReaderExtensionsTests
{
    [Theory]
    [InlineData([0x03, new byte[] { 0x03 }])]
    [InlineData([0x7F, new byte[] { 0x7F }])]
    [InlineData([0x80, new byte[] { 0x80, 0x80 }])]
    [InlineData([0x2E57, new byte[] { 0xAE, 0x57 }])]
    [InlineData([0x3FFF, new byte[] { 0xBF, 0xFF }])]
    [InlineData([0x4000, new byte[] { 0xC0, 0x00, 0x40, 0x00 }])]
    [InlineData([0x1FFFFFFF, new byte[] { 0xDF, 0xFF, 0xFF, 0xFF }])]
    public void ReadSignatureUInt_CompressedUIntRepresentation_ReadsAndDecompressesUInt(uint originalValue,
        byte[] compressedRepresentation)
    {
        // Arrange
        using var ms = new MemoryStream(compressedRepresentation);
        using var binaryReader = new BinaryReader(ms);
        var reader = new SharedReader(0, binaryReader);

        // Act
        var val = SignatureReaderExtensions.ReadSignatureUInt(reader);

        // Assert
        Assert.Equal(originalValue, val);
    }
    
    [Theory]
    [InlineData([3, new byte[] { 0x06 }])]
    [InlineData([-3, new byte[] { 0x7B }])]
    [InlineData([64, new byte[] { 0x80, 0x80 }])]
    [InlineData([-64, new byte[] { 0x01 }])]
    [InlineData([8192, new byte[] { 0xC0, 0x00, 0x40, 0x00 }])]
    [InlineData([-8192, new byte[] { 0x80, 0x01 }])]
    [InlineData([268435455, new byte[] { 0xDF, 0xFF, 0xFF, 0xFE }])]
    [InlineData([-268435456, new byte[] { 0xC0, 0x00, 0x00, 0x01 }])]
    public void ReadSignatureInt_CompressedIntRepresentation_ReadsAndDecompressesInt(int originalValue,
        byte[] compressedRepresentation)
    {
        // Arrange
        using var ms = new MemoryStream(compressedRepresentation);
        using var binaryReader = new BinaryReader(ms);
        var reader = new SharedReader(0, binaryReader);

        // Act
        var val = SignatureReaderExtensions.ReadSignatureInt(reader);

        // Assert
        Assert.Equal(originalValue, val);
    }
}