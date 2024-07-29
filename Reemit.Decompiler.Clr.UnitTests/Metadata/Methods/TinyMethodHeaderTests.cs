using Reemit.Common;
using Reemit.Decompiler.Clr.Methods;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata.Methods;

public class TinyMethodHeaderTests
{
    [Fact]
    public async Task Read_ValidTinyMethodHeader_ReadsTinyMethodHeader()
    {
        // Arrange
        byte[] bytes =
        [
            // CodeSize
            0b00100000
            
            |
            
            // Type of header
            0b00000010
        ];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var header = TinyMethodHeader.Read(new SharedReader(0, reader));
        
        // Assert
        Assert.Equal(8u, header.CodeSize);
    }
}