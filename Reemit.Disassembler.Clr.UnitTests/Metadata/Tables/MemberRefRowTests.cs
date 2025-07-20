using Reemit.Disassembler.Clr.Metadata;
using Reemit.Disassembler.Clr.Metadata.Tables;

namespace Reemit.Disassembler.Clr.UnitTests.Metadata.Tables;

public class MemberRefRowTests
{
    [Fact]
    public async Task Read_ValidMemberRefRow_ReadsMemberRefRow()
    {
        // Arrange
        byte[] bytes = [0x09, 0x00, 0x12, 0x02, 0x01, 0x00];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var row = MemberRefRow.Read(1,
            new MetadataTableDataReader(reader, 0, new Dictionary<MetadataTableName, uint>
            {
                { MetadataTableName.TypeRef, 1 }
            }));

        // Assert
        Assert.Equal(MetadataTableName.TypeRef, row.Class.ReferencedTable);
        Assert.Equal(1u, row.Class.Rid);
        Assert.Equal(530u, row.Name);
        Assert.Equal(1u, row.Signature);
    }
}