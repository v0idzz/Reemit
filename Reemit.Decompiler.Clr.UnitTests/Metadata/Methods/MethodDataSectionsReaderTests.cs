using Reemit.Common;
using Reemit.Decompiler.Clr.Methods;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata.Methods;

public class MethodDataSectionsReaderTests
{
    [Fact]
    public async Task ReadMethodDataSections_SharedReaderNotPaddedTo4ByteBoundary_AppliesPaddingAndReadsSections()
    {
        // Arrange
        byte[] bytes =
        [
            // Padding
            0x00, 0x00, 0x00, 0x00,

            // MoreSects
            0b10000000
            |

            // SmallExceptionHeader
            0x01,
            0x10, 0x00, 0x00, 0x02, 0x00, 0x13, 0x00, 0x23, 0x36, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x0,

            // FatExceptionHeader
            0x41,
            0x1C, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x21, 0x02, 0x00, 0x00, 0x28, 0x02, 0x00, 0x00, 0x36, 0x00,
            0x00, 0x00, 0x1F, 0x00, 0x00, 0x01
        ];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var sections = MethodDataSectionsReader.ReadMethodDataSections(new SharedReader(2, reader))
            .ToArray();

        // Assert
        Assert.Collection(sections,
            s1 => Assert.IsType<SmallExceptionHeader>(s1),
            s2 => Assert.IsType<FatExceptionHeader>(s2));
    }
}