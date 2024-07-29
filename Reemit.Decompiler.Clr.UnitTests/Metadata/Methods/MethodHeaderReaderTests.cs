using Reemit.Common;
using Reemit.Decompiler.Clr.Methods;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata.Methods;

public class MethodHeaderReaderTests
{
    [Fact]
    public async Task ReadMethodHeader_TinyFormatBitsSet_ReadsTinyFormatHeader()
    {
        // Arrange
        byte[] bytes = 
        [
            // Type of header
            0b00000010

            |

            // CodeSize (see TinyMethodHeaderTests)
            0b00100000
        ];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var header = MethodHeaderReader.ReadMethodHeader(new SharedReader(0, reader));

        // Assert
        Assert.IsType<TinyMethodHeader>(header);
    }
    
    [Fact]
    public async Task ReadMethodHeader_FatFormatBitsSet_ReadsFatFormatHeader()
    {
        // Arrange
        byte[] bytes = 
        [
            // Type of header
            0b00000011

            |

            // FatMethodHeader (see FatMethodHeaderTests)
            0b00001000, 0x30,
            0x02, 0x00,
            0x40, 0x00, 0x00, 0x00,
            0x14, 0x00, 0x00, 0x11
        ];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var header = MethodHeaderReader.ReadMethodHeader(new SharedReader(0, reader));

        // Assert
        Assert.IsType<FatMethodHeader>(header);
    }
}