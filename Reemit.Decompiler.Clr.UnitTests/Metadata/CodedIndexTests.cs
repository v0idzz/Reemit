using Reemit.Decompiler.Clr.Metadata;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata;

public sealed class CodedIndexTests
{
    [Fact]
    public async Task Constructor_CodedIndexBytes_DecodesCodedIndex()
    {
        // Arrange
        await using var memoryStream = new MemoryStream([0x21, 0x03]);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var codedIndex = new CodedIndex(reader, CodedIndexTagFamily.HasConstant,
            new Dictionary<MetadataTableName, uint>
            {
                { MetadataTableName.Field, 3 },
                { MetadataTableName.Param, 3 },
                { MetadataTableName.Property, 3 }
            });
        
        // Assert
        Assert.Equal(0xC8u, codedIndex.Rid);
        Assert.Equal(MetadataTableName.Param, codedIndex.ReferencedTable);
    }
}