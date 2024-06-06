using Reemit.Common;
using Reemit.Decompiler.Clr.Metadata;
using Reemit.Decompiler.Clr.Metadata.Streams;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata.Streams;

public class MetadataTablesStreamTests
{
    [Fact]
    public async Task Constructor_ValidMetadataTablesStream_ReadsMetadataTablesStream()
    {
        // Arrange
        await using var fileStream = File.OpenRead("Resources/metadatatablesstream.bin");
        using var reader = new BinaryReader(fileStream);
        using var sharedReader = new SharedReader(0, reader, new());

        // Act
        var header = new MetadataTablesStream(sharedReader);
        
        // Assert
        Assert.Equal(0u, header.Reserved);
        Assert.Equal(2, header.MajorVersion);
        Assert.Equal(0, header.MinorVersion);
        Assert.Equal((HeapSizes)0, header.HeapSizes);
        Assert.Equal(1, header.Reserved1);
    }
}