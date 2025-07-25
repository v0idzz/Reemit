using Reemit.Disassembler.Clr.Metadata;
using Reemit.Disassembler.Clr.Metadata.Tables;

namespace Reemit.Disassembler.Clr.UnitTests.Metadata.Tables;

public class TypeDefRowTests
{
    [Fact]
    public async Task Read_ValidTypeDefRow_ReadsTypeDefRow()
    {
        // Arrange
        byte[] bytes = [0x01, 0x00, 0x10, 0x00, 0x01, 0x00, 0xEB, 0x01, 0x35, 0x00, 0x01, 0x00, 0x01, 0x00];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var row = TypeDefRow.Read(1, new MetadataTableDataReader(reader, 0, new Dictionary<MetadataTableName, uint>
        {
            { MetadataTableName.TypeRef, 1 },
            { MetadataTableName.Field, 1 },
            { MetadataTableName.MethodDef, 1 }
        }));
        
        // Assert
        Assert.Equal(TypeClassLayoutAttributes.AutoLayout, row.ClassLayout);
        Assert.Equal(TypeClassSemanticsAttributes.Class, row.ClassSemantics);
        Assert.Equal((TypeImplementationAttributes)0, row.Implementation);
        Assert.Equal(TypeStringFormattingAttributes.AnsiClass, row.StringFormatting);
        Assert.Equal(TypeVisibilityAttributes.Public, row.Visibility);
        Assert.Equal(1048577u, row.Flags);
        Assert.Equal(1u, row.TypeName);
        Assert.Equal(491u, row.TypeNamespace);
        Assert.Equal(MetadataTableName.TypeRef, row.Extends.ReferencedTable);
        Assert.Equal(13u, row.Extends.Rid);
        Assert.Equal(1u, row.FieldList);
        Assert.Equal(1u, row.MethodList);
    }
}