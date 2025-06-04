using Reemit.Common;
using Reemit.Disassembler.Clr.Methods;

namespace Reemit.Disassembler.Clr.UnitTests.Metadata.Methods;

public class FatExceptionClauseTests
{
    [Fact]
    public async Task Read_ValidFatExceptionClause_ReadsFatExceptionClause()
    {
        // Arrange
        byte[] bytes =
        [
            // Flags
            0x00, 0x00, 0x00, 0x00,
            
            // TryOffset
            0x07, 0x00, 0x00, 0x00,
            
            // TryLength
            0x21, 0x02, 0x00, 0x00,
            
            // HandlerOffset
            0x28, 0x02, 0x00, 0x00,
            
            // HandlerLength
            0x36, 0x00, 0x00, 0x00,
            
            // ClassTokenOrFilterOffset
            0x1F, 0x00, 0x00, 0x01
        ];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var clause = FatExceptionClause.Read(new SharedReader(0, reader));
        
        // Assert
        Assert.Equal(CorILExceptionClauses.Exception, clause.Flags);
        Assert.Equal(7u, clause.TryOffset);
        Assert.Equal(545u, clause.TryLength);
        Assert.Equal(552u, clause.HandlerOffset);
        Assert.Equal(54u, clause.HandlerLength);
        Assert.Equal(16777247u, clause.ClassTokenOrFilterOffset);
    }
}