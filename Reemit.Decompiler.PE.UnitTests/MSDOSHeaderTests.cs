namespace Reemit.Decompiler.PE.UnitTests;

public sealed class MSDOSHeaderTests
{
    [Fact]
    public async Task Constructor_ValidMSDOSHeader_ConstructsMSDOSHeader()
    {
        // Arrange
        await using var fileStream = File.OpenRead("Resources/msdosheader.bin");
        using var reader = new BinaryReader(fileStream);

        // Act
        var header = new MSDOSHeader(reader);

        // Assert
        Assert.Equal(MSDOSHeader.ExpectedStartingBytes, header.StartingBytes);
        Assert.Equal(MSDOSHeader.ExpectedEndingBytes, header.EndingBytes);
        Assert.Equal(0x00000080u, header.Lfanew);
    }
}