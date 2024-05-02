using Reemit.Decompiler.Cli.Metadata;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata;

public sealed class MetadataRootTests
{
    [Fact]
    public async Task Constructor_ValidMetadataRootHeader_ReadsMetadataRoot()
    {
        // Arrange
        await using var fileStream = File.OpenRead("Resources/metadataroot.bin");
        using var reader = new BinaryReader(fileStream);

        // Act
        var header = new MetadataRoot(reader);
        
        // Assert
        Assert.Equal(0x424A5342u, header.Signature);
        Assert.Equal(1u, header.MajorVersion);
        Assert.Equal(1u, header.MinorVersion);
        Assert.Equal(0u, header.Reserved);
        Assert.Equal("v4.0.30319", header.Version);
        Assert.Equal(0u, header.Flags);
        Assert.Equal(5u, header.Streams);
    }
}