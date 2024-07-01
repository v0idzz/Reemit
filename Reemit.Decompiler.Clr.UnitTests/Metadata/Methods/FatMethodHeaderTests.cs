using Reemit.Common;
using Reemit.Decompiler.Clr.Methods;

namespace Reemit.Decompiler.Clr.UnitTests.Metadata.Methods;

public class FatMethodHeaderTests
{
    [Fact]
    public async Task Read_ValidFatMethodHeader_ReadsFatMethodHeader()
    {
        // Arrange
        byte[] bytes = 
        [
            // Flags (12 bits) and Size (4 bits)
            0x13, 0x30, 
            
            // MaxStack
            0x02, 0x00,
            
            // CodeSize
            0x40, 0x00, 0x00, 0x00,
            
            // LocalVarSigTok
            0x14, 0x00, 0x00, 0x11
        ];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var header =
            FatMethodHeader.Read(new SharedReader(0, reader, new object()));

        // Assert
        Assert.True(header.Flags.HasFlag(CorILMethodFlags.InitLocals));
        Assert.Equal(CorILMethodFormat.Fat, (CorILMethodFormat)(header.Flags & CorILMethodFlags.FormatMask));
        Assert.Equal(3u, header.Size);
        Assert.Equal(2u, header.MaxStack);
        Assert.Equal(64u, header.CodeSize);
        Assert.Equal(285212692u, header.LocalVarSigTok);
    }
}