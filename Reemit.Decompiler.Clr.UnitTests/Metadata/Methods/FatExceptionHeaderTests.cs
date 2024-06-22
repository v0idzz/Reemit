using Reemit.Common;
using Reemit.Decompiler.Clr.Methods;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata.Methods;

public class FatExceptionHeaderTests
{
    [Fact]
    public async Task Read_ValidFatExceptionHeader_ReadsFatExceptionHeader()
    {
        // Arrange
        byte[] bytes =
        [
            // Kind
            0x41,
            
            // DataSize
            0x1C, 0x00, 0x00,
            
            // Clause (see FatExceptionClauseTests)
            0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x21, 0x02, 0x00, 0x00, 0x28, 0x02,
            0x00, 0x00, 0x36, 0x00, 0x00, 0x00, 0x1F, 0x00, 0x00, 0x01
        ];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);
        
        // Act
        var header = FatExceptionHeader.Read(new SharedReader(0, reader, new object()));
        
        // Assert
        Assert.Equal(CorILMethodSectionFlags.EHTable | CorILMethodSectionFlags.FatFormat, header.Kind);
        Assert.Equal(28u, header.DataSize);
        Assert.Single(header.Clauses);
    }
}