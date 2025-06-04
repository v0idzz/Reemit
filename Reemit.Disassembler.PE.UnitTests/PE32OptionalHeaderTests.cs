using Reemit.Disassembler.PE.Enums;

namespace Reemit.Disassembler.PE.UnitTests;

public sealed class PE32OptionalHeaderTests
{
    [Fact]
    public async Task Constructor_ValidPE32OptionalHeader_ConstructsPE32OptionalHeader()
    {
        // Arrange
        await using var fileStream = File.OpenRead("Resources/pe32optionalheader.bin");
        using var reader = new BinaryReader(fileStream);
        
        // Act
        var header = new PE32OptionalHeader(reader);
        
        // Assert
        Assert.Equal(OptionalHeaderMagic.PE32, header.Magic);
        Assert.Equal(48, header.MajorLinkerVersion);
        Assert.Equal(0, header.MinorLinkerVersion);
        Assert.Equal(12800u, header.SizeOfCode);
        Assert.Equal(2048u, header.SizeOfInitializedData);
        Assert.Equal(0u, header.SizeOfUninitializedData);
        Assert.Equal(0x000050FEu, header.AddressOfEntryPoint);
        Assert.Equal(0x00002000u, header.BaseOfCode);
        Assert.Equal(0x00006000u, header.BaseOfData);
        Assert.Equal(0x00400000u, header.WindowsSpecificFields.ImageBase);
        Assert.Equal(8192u, header.WindowsSpecificFields.SectionAlignment);
        Assert.Equal(512u, header.WindowsSpecificFields.FileAlignment);
        Assert.Equal(4, header.WindowsSpecificFields.MajorOperatingSystemVersion);
        Assert.Equal(0, header.WindowsSpecificFields.MinorOperatingSystemVersion);
        Assert.Equal(0, header.WindowsSpecificFields.MajorImageVersion);
        Assert.Equal(0, header.WindowsSpecificFields.MinorImageVersion);
        Assert.Equal(4, header.WindowsSpecificFields.MajorSubsystemVersion);
        Assert.Equal(0, header.WindowsSpecificFields.MinorSubsystemVersion);
        Assert.Equal(0u, header.WindowsSpecificFields.Win32VersionValue);
        Assert.Equal(0x0000A000u, header.WindowsSpecificFields.SizeOfImage);
        Assert.Equal(0x00000200u, header.WindowsSpecificFields.SizeOfHeaders);
        Assert.Equal(0u, header.WindowsSpecificFields.CheckSum);
        Assert.Equal(WindowsSubsystem.ImageSubsystemWindowsCui, header.WindowsSpecificFields.Subsystem);
        Assert.Equal(
            DllCharacteristics.ImageDllCharacteristicsHighEntropyVa |
            DllCharacteristics.ImageDllCharacteristicsDynamicBase | DllCharacteristics.ImageDllCharacteristicsNxCompat |
            DllCharacteristics.ImageDllCharacteristicsNoSeh |
            DllCharacteristics.ImageDllCharacteristicsTerminalServerAware,
            header.WindowsSpecificFields.DllCharacteristics);
        Assert.Equal(0x00100000u, header.WindowsSpecificFields.SizeOfStackReserve);
        Assert.Equal(0x00001000u, header.WindowsSpecificFields.SizeOfStackCommit);
        Assert.Equal(0x00100000u, header.WindowsSpecificFields.SizeOfHeapReserve);
        Assert.Equal(0x00001000u, header.WindowsSpecificFields.SizeOfHeapCommit);
        Assert.Equal(0u, header.WindowsSpecificFields.LoaderFlags);
        Assert.Equal(16u, header.WindowsSpecificFields.NumberOfRvaAndSizes);
        
        Assert.Collection(header.WindowsSpecificFields.DataDirectories,
            x => Assert.Equal(PETestsHelper.EmptyImageDataDirectory, x),
            x =>
            {
                Assert.Equal(79u, x.Size);
                Assert.Equal(0x000050A9u, x.VirtualAddress);
            },
            x =>
            {
                Assert.Equal(1300u, x.Size);
                Assert.Equal(0x00006000u, x.VirtualAddress);
            },
            x => Assert.Equal(PETestsHelper.EmptyImageDataDirectory, x),
            x => Assert.Equal(PETestsHelper.EmptyImageDataDirectory, x),
            x =>
            {
                Assert.Equal(12u, x.Size);
                Assert.Equal(0x00008000u, x.VirtualAddress);
            },
            x =>
            {
                Assert.Equal(84u, x.Size);
                Assert.Equal(0x00004FD4u, x.VirtualAddress);
            },
            x => Assert.Equal(PETestsHelper.EmptyImageDataDirectory, x),
            x => Assert.Equal(PETestsHelper.EmptyImageDataDirectory, x),
            x => Assert.Equal(PETestsHelper.EmptyImageDataDirectory, x),
            x => Assert.Equal(PETestsHelper.EmptyImageDataDirectory, x),
            x => Assert.Equal(PETestsHelper.EmptyImageDataDirectory, x),
            x =>
            {
                Assert.Equal(8u, x.Size);
                Assert.Equal(0x00002000u, x.VirtualAddress);
            },
            x => Assert.Equal(PETestsHelper.EmptyImageDataDirectory, x),
            x =>
            {
                Assert.Equal(72u, x.Size);
                Assert.Equal(0x00002008u, x.VirtualAddress);
            },
            x => Assert.Equal(PETestsHelper.EmptyImageDataDirectory, x));
    }
}