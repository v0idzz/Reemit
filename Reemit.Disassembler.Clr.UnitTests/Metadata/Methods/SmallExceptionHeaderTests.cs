using Reemit.Common;
using Reemit.Disassembler.Clr.Metadata;
using Reemit.Disassembler.Clr.Metadata.Tables;
using Reemit.Disassembler.Clr.Methods;

namespace Reemit.Disassembler.Clr.UnitTests.Metadata.Methods;

public class SmallExceptionHeaderTests
{
    [Fact]
    public async Task Read_ValidSmallExceptionHeader_ReadsSmallExceptionHeader()
    {
        // Arrange
        byte[] bytes =
        [
            // Kind
            0x01,

            // DataSize
            0x10,

            // Reserved
            0x00, 0x00, 0x00, 0x00,

            // Clause (see SmallExceptionClauseTests)
            0x07, 0x00, 0xBA, 0xC1, 0x00, 0x21, 0x1F, 0x00, 0x00, 0x01
        ];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var header = SmallExceptionHeader.Read(new SharedReader(0, reader));

        // Assert
        Assert.Equal(CorILMethodSectionFlags.EHTable, header.Kind);
        Assert.Equal(16u, header.DataSize);
        Assert.Equal(0u, header.Reserved);
        Assert.Single(header.Clauses);
    }

    [Fact]
    public async Task Read_SmallExceptionHeaderWithNonZeroReservedBytes_Throws()
    {
        // Arrange
        byte[] bytes =
        [
            // Kind
            0x01,

            // DataSize
            0x10,

            // Reserved
            0xFF, 0xFF, 0xFF, 0xFF,

            // Clause (see SmallExceptionClauseTests)
            0x07, 0x00, 0xBA, 0xC1, 0x00, 0x21, 0x1F, 0x00, 0x00, 0x01
        ];
        await using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);

        // Act
        var act = () =>
            SmallExceptionHeader.Read(new MetadataTableDataReader(reader, 0,
                new Dictionary<MetadataTableName, uint>()));

        // Assert
        Assert.Throws<BadImageFormatException>(act);
    }
}