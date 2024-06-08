using Reemit.Decompiler.Clr.Metadata;
using Reemit.Decompiler.Clr.Metadata.Tables;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata.Tables;

public class TypeDefRowTests
{
    [Fact]
    public async Task Constructor_ValidTypeDefRow_ReadsTypeDefRow()
    {
        // Arrange
        byte[] bytes = [0x01, 0x00, 0x10, 0x00, 0x01, 0x00, 0xEB, 0x01, 0x35, 0x00, 0x01, 0x00, 0x01, 0x00];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var row = TypeDefRow.Read(new MetadataTableDataReader(reader, 0, new Dictionary<MetadataTableName, uint>
        {
            { MetadataTableName.TypeRef, 1 },
            { MetadataTableName.Field, 1 },
            { MetadataTableName.MethodDef, 1 }
        }));
        
        // Assert
        Assert.Equal(TypeAttributes.Public, row.Flags);
        Assert.Equal(1u, row.TypeName);
        Assert.Equal(491u, row.TypeNamespace);
        Assert.Equal(MetadataTableName.TypeRef, row.Extends.ReferencedTable);
        Assert.Equal(13u, row.Extends.Rid);
        Assert.Equal(1u, row.FieldList);
        Assert.Equal(1u, row.MethodList);
    }
}