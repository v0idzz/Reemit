using Reemit.Decompiler.Clr.Disassembler;

namespace Reemit.Decompiler.Clr.UnitTests.Disassembler;

public class OpcodeOperandTableTests
{
    [Fact]
    public void ResolveOperandType_Standard_OperandTypeResolved()
    {
        // Arrange
        var opcode = Opcode.call;
        OpcodeInfo opcodeInfo = opcode;

        // Act
        var actual1 = OpcodeOperandTable.GetOperandType(opcode);
        var actual2 = OpcodeOperandTable.GetOperandType(opcodeInfo);

        // Assert
        Assert.Equal(OperandType.MetadataToken, actual1);
        Assert.Equal(OperandType.MetadataToken, actual2);
    }

    [Fact]
    public void ResolveOperandType_Extended_OperandTypeResolved()
    {
        // Arrange
        var opcode = ExtendedOpcode.constrained;
        OpcodeInfo opcodeInfo = opcode;

        // Act
        var actual1 = OpcodeOperandTable.GetOperandType(opcode);
        var actual2 = OpcodeOperandTable.GetOperandType(opcodeInfo);

        // Assert
        Assert.Equal(OperandType.MetadataToken, actual1);
        Assert.Equal(OperandType.MetadataToken, actual2);
    }

    [Fact]
    public void ResolveOperandType_Standard_OperandTypeNotResolved()
    {
        // Arrange
        var opcode = Opcode.add;
        OpcodeInfo opcodeInfo = opcode;

        // Act
        var actual1 = OpcodeOperandTable.GetOperandType(opcode);
        var actual2 = OpcodeOperandTable.GetOperandType(opcodeInfo);

        // Assert
        Assert.Equal(OperandType.None, actual1);
        Assert.Equal(OperandType.None, actual2);
    }
}
