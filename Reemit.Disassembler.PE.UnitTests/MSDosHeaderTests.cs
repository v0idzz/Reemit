namespace Reemit.Disassembler.PE.UnitTests;

public sealed class MSDosHeaderTests
{
    [Fact]
    public async Task Constructor_ValidMSDosHeader_ConstructsMSDosHeader()
    {
        // Arrange
        await using var fileStream = File.OpenRead("Resources/msdosheader.bin");
        using var reader = new BinaryReader(fileStream);

        // Act
        var header = new MSDosHeader(reader);

        // Assert
        Assert.Equal(MSDosHeader.ExpectedStartingBytes, header.StartingBytes);
        Assert.Equal(MSDosHeader.ExpectedEndingBytes, header.EndingBytes);
        Assert.Equal(0x00000080u, header.Lfanew);
    }
}