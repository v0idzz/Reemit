using System.Collections.Immutable;

namespace Reemit.Decompiler.Clr.Disassembler;

public class StreamDisassembler(Stream stream)
{
    private InstructionDecoder _decoder = new(stream);

    public IReadOnlyCollection<Instruction> Disassemble()
    {
        var instructions = new List<Instruction>();

        while (stream.Position < stream.Length)
        {
            instructions.Add(_decoder.Decode());
        }

        return instructions.ToImmutableArray();
    }
}
