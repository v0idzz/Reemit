using Reemit.Decompiler.PE.Enums;

namespace Reemit.Decompiler.PE.UnitTests;

public sealed class PEHeaderTests
{
    [Fact]
    public async Task Constructor_ValidPEHeader_ReadsPEHeader()
    {
        // Arrange
        await using var fileStream = File.OpenRead("Resources/peheader.bin");
        using var reader = new BinaryReader(fileStream);

        // Act
        var header = new CoffHeader(reader);

        // Assert
        Assert.Equal(CoffHeader.SignatureValue, header.Signature);
        Assert.Equal(MachineType.ImageFileMachineI386, header.Machine);
        Assert.Equal(3, header.NumberOfSections);
        Assert.Equal(2592383461, header.TimeDateStamp);
        Assert.Equal(0, header.PointerToSymbolTable);
        Assert.Equal(0, header.NumberOfSymbols);
        Assert.Equal(224, header.SizeOfOptionalHeader);
        Assert.Equal(PECharacteristics.ImageFileLargeAddressAware | PECharacteristics.ImageFileExecutableImage,
            header.Characteristics);
    }
}