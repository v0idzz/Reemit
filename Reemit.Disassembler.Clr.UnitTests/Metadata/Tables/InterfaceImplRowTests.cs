using Reemit.Disassembler.Clr.Metadata;
using Reemit.Disassembler.Clr.Metadata.Tables;

namespace Reemit.Disassembler.Clr.UnitTests.Metadata.Tables;

public class InterfaceImplRowTests
{
    [Fact]
    public async Task Read_ValidInterfaceImplRow_ReadsInterfaceImplRow()
    {
        // Arrange
        byte[] bytes = [0x02, 0x00, 0x10, 0x00];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var row = InterfaceImplRow.Read(1,
            new MetadataTableDataReader(reader, 0, new Dictionary<MetadataTableName, uint>
            {
                { MetadataTableName.TypeDef, 1 },
                { MetadataTableName.TypeRef, 0 }
            }));

        // Assert
        Assert.Equal(2u, row.Class);
        Assert.Equal(4u, row.Interface.Rid);
        Assert.Equal(MetadataTableName.TypeDef, row.Interface.ReferencedTable);
    }
}