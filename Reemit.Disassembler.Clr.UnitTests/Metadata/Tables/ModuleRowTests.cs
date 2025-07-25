using Reemit.Disassembler.Clr.Metadata;
using Reemit.Disassembler.Clr.Metadata.Tables;

namespace Reemit.Disassembler.Clr.UnitTests.Metadata.Tables;

public class ModuleRowTests
{
    [Fact]
    public async Task Read_ValidModuleRow_ReadsModuleRow()
    {
        // Arrange
        byte[] bytes = [0x00, 0x00, 0x68, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var header = ModuleRow.Read(1, new MetadataTableDataReader(reader, 0, new Dictionary<MetadataTableName, uint>()));
        
        // Assert
        Assert.Equal([0, 0], header.Generation);
        Assert.Equal(360u, header.Name);
        Assert.Equal(1u, header.Mvid);
        Assert.Equal(0u, header.EncId);
        Assert.Equal(0u, header.EncBaseId);
    }
}