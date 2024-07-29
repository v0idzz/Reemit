using Reemit.Common;
using Reemit.Decompiler.Clr.Methods;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata.Methods;

public class SmallExceptionClauseTests
{
    [Fact]
    public async Task Read_ValidSmallExceptionClause_ReadsSmallExceptionClause()
    {
        // Arrange
        byte[] bytes = 
        [
            // Flags
            0x00, 0x00,
            
            // TryOffset
            0x07, 0x00,
            
            // TryLength
            0xBA,
            
            // HandlerOffset
            0xC1, 0x00,
            
            // HandlerLength
            0x21,
            
            // ClassTokenOrFilterOffset
            0x1F, 0x00, 0x00, 0x01
        ];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var clause = SmallExceptionClause.Read(new SharedReader(0, reader));
        
        // Assert
        Assert.Equal(CorILExceptionClauses.Exception, clause.Flags);
        Assert.Equal(7u, clause.TryOffset);
        Assert.Equal(186u, clause.TryLength);
        Assert.Equal(193u, clause.HandlerOffset);
        Assert.Equal(33u, clause.HandlerLength);
        Assert.Equal(16777247u, clause.ClassTokenOrFilterOffset);
    }
}