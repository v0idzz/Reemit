using Reemit.Decompiler.Clr.Metadata;
using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata.Tables;

public class ParamRowTests
{
    [Fact]
    public async Task Read_ValidFieldRow_ReadsFieldRow()
    {
        // Arrange
        byte[] bytes = [0x00, 0x00, 0x01, 0x00, 0x10, 0x00];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var row = ParamRow.Read(1, new MetadataTableDataReader(reader, 0, new Dictionary<MetadataTableName, uint>()));
        
        // Assert
        Assert.Equal(0u, (uint)row.Flags);
        Assert.Equal(1u, row.Sequence);
        Assert.Equal(16u, row.Name);
    }
}