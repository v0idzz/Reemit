namespace Reemit.Common.UnitTests;

public sealed class SharedReaderScopeTests
{
    [Fact]
    public void CreateScope_Called_TracksRange()
    {
        // Arrange
        byte[] bytes = [0x0, 0x1, 0x2, 0x3, 0x4, 0x5];
        using var stream = new MemoryStream(bytes);
        using var binaryReader = new BinaryReader(stream);
        const int sharedReaderOffset = 1;
        using var sharedReader = new SharedReader(sharedReaderOffset, binaryReader, new object());
        const int readBytesCount = 2;
        byte[] actualBytes;
        RangeMapped<byte[]> actualRangeMappedBytes;

        // Act
        using (var scope = sharedReader.CreateRangeScope())
        {
            actualBytes = sharedReader.ReadBytes(readBytesCount);
            actualRangeMappedBytes = scope.ToRangeMapped(actualBytes);
        }

        // Assert
        Assert.Equal(sharedReaderOffset + readBytesCount, sharedReader.Offset);
        Assert.Equal(readBytesCount, sharedReader.RelativeOffset);
        Assert.Equal(readBytesCount, actualBytes.Length);
        Assert.Equal([0x1, 0x2], actualBytes);
        Assert.Equal(1, actualRangeMappedBytes.Position);
        Assert.Equal(2, actualRangeMappedBytes.Length);
        Assert.Equal(readBytesCount, actualRangeMappedBytes.Value.Length);
        Assert.Equal([0x1, 0x2], actualRangeMappedBytes.Value);
    }

    [Fact]
    public void CreateNestedScope_Called_TracksRange()
    {
        // Arrange
        byte[] bytes = [0x0, 0x1, 0x2, 0x3, 0x4, 0x5];
        using var stream = new MemoryStream(bytes);
        using var binaryReader = new BinaryReader(stream);
        const int sharedReaderOffset = 1;
        using var sharedReader = new SharedReader(sharedReaderOffset, binaryReader, new object());
        const int readBytesCount = 2;
        byte[] actualOuterBytes = new byte[readBytesCount], actualInnerBytes;
        RangeMapped<byte[]> actualRangeMappedOuterBytes, actualRangeMappedInnerBytes;

        // Act
        using (var outerScope = sharedReader.CreateRangeScope())
        {
            actualOuterBytes[0] = sharedReader.ReadByte();

            using (var innerScope = sharedReader.CreateRangeScope())
            {
                actualInnerBytes = sharedReader.ReadBytes(readBytesCount);
                actualRangeMappedInnerBytes = innerScope.ToRangeMapped(actualInnerBytes);
            }

            actualOuterBytes[1] = sharedReader.ReadByte();

            actualRangeMappedOuterBytes = outerScope.ToRangeMapped(actualOuterBytes);
        }

        // Assert
        Assert.Equal(sharedReaderOffset + readBytesCount * 2, sharedReader.Offset);
        Assert.Equal(readBytesCount * 2, sharedReader.RelativeOffset);
        
        Assert.Equal([0x1, 0x4], actualOuterBytes);
        Assert.Equal([0x1, 0x4], actualRangeMappedOuterBytes.Value);
        Assert.Equal(1, actualRangeMappedOuterBytes.Position);
        Assert.Equal(readBytesCount * 2, actualRangeMappedOuterBytes.Length);
        
        Assert.Equal([0x2, 0x3], actualInnerBytes);
        Assert.Equal([0x2, 0x3], actualRangeMappedInnerBytes.Value);
        Assert.Equal(2, actualRangeMappedInnerBytes.Position);
        Assert.Equal(readBytesCount, actualRangeMappedInnerBytes.Length);
    }
}
