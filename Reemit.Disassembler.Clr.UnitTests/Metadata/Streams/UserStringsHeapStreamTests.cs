using Reemit.Common;
using Reemit.Disassembler.Clr.Metadata;
using Reemit.Disassembler.Clr.Metadata.Streams;

namespace Reemit.Disassembler.Clr.UnitTests.Metadata.Streams;

public class UserStringsHeapStreamTests
{
    [Theory]
    [InlineData(0x29u, "here goes some string")]
    [InlineData(0x1u, "another string here")]
    public async Task ReadString_Called_ReadsStringAtTheGivenOffset(uint offset, string expectedString)
    {
        // Arrange
        await using var fileStream = File.OpenRead("Resources/userstringsheapstream.bin");
        using var reader = new BinaryReader(fileStream);
        using var sharedReader = new SharedReader(0, reader);
        
        var stream = new UserStringsHeapStream(sharedReader,
            new StreamHeader(0u, (uint)fileStream.Length, UserStringsHeapStream.Name));
        
        // Act
        var result = stream.ReadString(offset);

        // Assert
        Assert.Equal(expectedString, result);
    }
}