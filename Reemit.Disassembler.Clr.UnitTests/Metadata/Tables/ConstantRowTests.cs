using Reemit.Disassembler.Clr.Metadata;
using Reemit.Disassembler.Clr.Metadata.Tables;
using Reemit.Disassembler.Clr.Signatures;

namespace Reemit.Disassembler.Clr.UnitTests.Metadata.Tables;

public class ConstantRowTests
{
    [Fact]
    public async Task Read_ValidConstantRow_ReadsConstantRow()
    {
        // Arrange
        byte[] bytes = [0x0E, 0x00, 0x08, 0x00, 0xD3, 0x9F, 0x00, 0x00];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var row = ConstantRow.Read(1,
            new MetadataTableDataReader(reader, 0, new Dictionary<MetadataTableName, uint>
            {
                { MetadataTableName.Field, 1 }
            }));
        
        // Assert
        Assert.Equal(ElementType.String, row.Type);
        Assert.Equal(MetadataTableName.Field, row.Parent.ReferencedTable);
        Assert.Equal(2u, row.Parent.Rid);
        Assert.Equal(40915u, row.Value);
    }
}