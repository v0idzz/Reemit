using Reemit.Decompiler.Clr.Metadata;
using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata.Tables;

public class FieldRowTests
{
    [Fact]
    public async Task Read_ValidFieldRow_ReadsFieldRow()
    {
        // Arrange
        byte[] bytes = [0x01, 0x00, 0x11, 0x00, 0x1E, 0x00];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var row = FieldRow.Read(1, new MetadataTableDataReader(reader, 0, new Dictionary<MetadataTableName, uint>()));
        
        // Assert
        Assert.Equal(FieldAttributes.Private, row.Flags);
        Assert.Equal(17u, row.Name);
        Assert.Equal(30u, row.Signature);
    }
}