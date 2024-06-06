using Reemit.Decompiler.Clr.Metadata;
using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata.Tables;

public class TypeRefRowTests
{
    [Fact]
    public async Task Constructor_ValidTypeRefRow_ReadsTypeRefRow()
    {
        // Arrange
        byte[] bytes = [0x06, 0x00, 0x2B, 0x00, 0xA2, 0x01];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var row = TypeRefRow.Read(new MetadataTableDataReader(reader, 0, new Dictionary<MetadataTableName, uint>
        {
            { MetadataTableName.AssemblyRef, 1 }
        }));
        
        // Assert
        Assert.Equal(MetadataTableName.AssemblyRef, row.ResolutionScope.Value.ReferencedTable);
        Assert.Equal(1u, row.ResolutionScope.Value.Rid);
        Assert.Equal(43u, row.TypeName);
        Assert.Equal(418u, row.TypeNamespace);
    }
}