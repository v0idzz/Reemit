using Reemit.Decompiler.PE.Enums;

namespace Reemit.Decompiler.PE.UnitTests;

public sealed class SectionHeaderTests
{
    [Fact]
    public async Task Constructor_ValidSectionHeader_ReadsSectionHeader()
    {
        // Arrange
        await using var fileStream = File.OpenRead("Resources/sectionheader.bin");
        using var reader = new BinaryReader(fileStream);
        
        // Act
        var header = new SectionHeader(reader);
        
        // Assert
        Assert.Equal(".text", header.Name);
        Assert.Equal(12548u, header.VirtualSize);
        Assert.Equal(0x00002000u, header.VirtualAddress);
        Assert.Equal(12800u, header.SizeOfRawData);
        Assert.Equal(0x00000200u, header.PointerToRawData);
        Assert.Equal(0u, header.PointerToRelocations);
        Assert.Equal(0u, header.PointerToLinenumbers);
        Assert.Equal(0u, header.NumberOfRelocations);
        Assert.Equal(0u, header.NumberOfLinenumbers);
        Assert.Equal(
            SectionFlags.ImageScnCntCode | SectionFlags.ImageScnMemExecute | SectionFlags.ImageScnMemRead,
            header.Characteristics);
    }
}