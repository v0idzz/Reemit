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
            0b100000
            
            |
            
            // Type of header
            0b10
        ];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var header = TinyMethodHeader.Read(new SharedReader(0, reader, new object()));
        
        // Assert
        Assert.Equal(32u, header.CodeSize);
    }
}