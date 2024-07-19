using Reemit.Decompiler.Clr.Disassembler;

namespace Reemit.Decompiler.Clr.UnitTests.Disassembler;

public class StreamDisassemblerTests
{
    [Theory]
    [MemberData(nameof(GetInstructionTestCases))]
    public void Disassemble_Buffer_InstructionsDisassembled(
        IEnumerable<Instruction> expectedInstructions,
        byte[] bytecode)
    {
        // Arrange
        using var stream = new MemoryStream(bytecode);
        var disassembler = new StreamDisassembler(stream);

        // Act
        var actualInstructions = disassembler.Disassemble().Select(x => x.Value);

        // Assert
        Assert.Equal(expectedInstructions, actualInstructions, InstructionComparer.Instance);
    }

    public static IEnumerable<object[]> GetInstructionTestCases() =>
        new[]
        {
            CreateInstructionTestCase(
                [
                    new Instruction(Opcode.nop, Operand.None),
                    new Instruction(Opcode.nop, Operand.None),
                    new Instruction(Opcode.nop, Operand.None),
                ],
                [
                    0x00,
                    0x00,
                    0x00,
                ]),
            CreateInstructionTestCase(
                [
                    new Instruction(
                        Opcode.call,
                        new Operand(
                            OperandType.MetadataToken,
                            [ 0x50, 0x20, 0x30, 0x00 ])),
                ],
                [
                    0x28,
                    0x50,
                    0x20,
                    0x30,
                    0x00,
                ]),
            CreateInstructionTestCase(
                [
                    new Instruction(
                        Opcode.@switch,
                        new Operand(
                            OperandType.JumpTable,
                            [
                                0x02, 0x00, 0x00, 0x00, // Jump count
                                0x50, 0x20, 0x30, 0x00, // Jump 1
                                0x10, 0x50, 0x90, 0x20, // Jump 2
                            ])),
                ],
                [
                    0x45, // switch opcode
                    0x02, 0x00, 0x00, 0x00, // Jump count
                    0x50, 0x20, 0x30, 0x00, // Jump 1
                    0x10, 0x50, 0x90, 0x20, // Jump 2
                ])
        };

    private static object[] CreateInstructionTestCase(
        IEnumerable<Instruction> expectedInstructions,
        byte[] bytecode) =>
        new object[] { expectedInstructions, bytecode };
}
