using Reemit.Disassembler.PE.Enums;

namespace Reemit.Disassembler.PE.UnitTests;

public sealed class PEFileTests
{
    [Fact]
    public async Task GetStructureDescribedByDataDirectory_ValidPEImageWithCliHeader_ReadsCliHeader()
    {
        // Arrange
        await using var fileStream = File.OpenRead("Resources/NetAssembly.dll");
        using var reader = new BinaryReader(fileStream);
        var peFile = new PEFile(reader);
        var cliHeaderDataDirectory = peFile.DataDirectories.ElementAt(^2);

        // Act
        var cliHeader = peFile.GetStructureDescribedByDataDirectory<CliHeader>(cliHeaderDataDirectory);

        // Assert
        Assert.Equal(72L, cliHeader.Cb);
        Assert.Equal(2u, cliHeader.MajorRuntimeVersion);
        Assert.Equal(5u, cliHeader.MinorRuntimeVersion);
        Assert.Equal(1192u, cliHeader.Metadata.Size);
        Assert.Equal(8280u, cliHeader.Metadata.VirtualAddress);
        Assert.Equal(RuntimeFlags.ILOnly, cliHeader.Flags);
        Assert.Equal(0u, cliHeader.EntryPointToken);
        Assert.Equal(PETestsHelper.EmptyImageDataDirectory, cliHeader.Resources);
        Assert.Equal(PETestsHelper.EmptyImageDataDirectory, cliHeader.StrongNameSignature);
        Assert.Equal(PETestsHelper.EmptyImageDataDirectory, cliHeader.CodeManagerTable);
        Assert.Equal(PETestsHelper.EmptyImageDataDirectory, cliHeader.VTableFixups);
        Assert.Equal(PETestsHelper.EmptyImageDataDirectory, cliHeader.ExportAddressTableJumps);
        Assert.Equal(PETestsHelper.EmptyImageDataDirectory, cliHeader.ManagedNativeHeader);
    }
}