using Reemit.Disassembler.Clr.Metadata;

namespace Reemit.Disassembler.Clr.UnitTests.Metadata;

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
        Assert.Collection(header.StreamHeaders,
            h =>
            {
                Assert.Equal("#~", h.Name);
                Assert.Equal(108u, h.Offset);
                Assert.Equal(368u, h.Size);
            },
            h =>
            {
                Assert.Equal("#Strings", h.Name);
                Assert.Equal(476u, h.Offset);
                Assert.Equal(492u, h.Size);
            },
            h =>
            {
                Assert.Equal("#US", h.Name);
                Assert.Equal(968u, h.Offset);
                Assert.Equal(4u, h.Size);
            },
            h =>
            {
                Assert.Equal("#GUID", h.Name);
                Assert.Equal(972u, h.Offset);
                Assert.Equal(16u, h.Size);
            },
            h =>
            {
                Assert.Equal("#Blob", h.Name);
                Assert.Equal(988u, h.Offset);
                Assert.Equal(204u, h.Size);
            });
    }
}