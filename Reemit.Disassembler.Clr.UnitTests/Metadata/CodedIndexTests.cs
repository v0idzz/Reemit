using Reemit.Disassembler.Clr.Metadata;

namespace Reemit.Disassembler.Clr.UnitTests.Metadata;

public sealed class CodedIndexTests
{
    [Fact]
    public async Task Constructor_CodedIndexBytes_Decodes16BitCodedIndex()
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

    [Fact]
    public async Task Constructor_CodedIndexBytes_Decodes32BitCodedIndex()
    {
        // Arrange
        await using var memoryStream = new MemoryStream([0x09, 0x00, 0x00, 0x00]);
        using var reader = new BinaryReader(memoryStream);
        
        // Act
        var codedIndex = new CodedIndex(reader, CodedIndexTagFamily.MemberRefParent,
            new Dictionary<MetadataTableName, uint>
            {
                { MetadataTableName.TypeDef, (uint)(Math.Pow(2, 13) + 1) }
            });
        
        // Assert
        Assert.Equal(0x1u, codedIndex.Rid);
        Assert.Equal(MetadataTableName.TypeRef, codedIndex.ReferencedTable);
    }
}