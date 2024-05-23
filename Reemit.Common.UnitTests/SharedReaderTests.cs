namespace Reemit.Common.UnitTests;

public sealed class SharedReaderTests
{
    [Fact]
    public void ReadBytes_Called_AdvancesRelativeOffsetAndOffset()
    {
        // Arrange
        byte[] bytes = [0x0, 0x1, 0x2, 0x3, 0x4, 0x5];
        using var stream = new MemoryStream(bytes);
        using var binaryReader = new BinaryReader(stream);
        const int sharedReaderOffset = 1;
        using var sharedReader = new SharedReader(sharedReaderOffset, binaryReader, new object());
        const int readBytesCount = 2;
        
        // Act
        sharedReader.ReadBytes(readBytesCount);
        
        // Assert
        Assert.Equal(sharedReaderOffset + readBytesCount, sharedReader.Offset);
        Assert.Equal(readBytesCount, sharedReader.RelativeOffset);
    }
}