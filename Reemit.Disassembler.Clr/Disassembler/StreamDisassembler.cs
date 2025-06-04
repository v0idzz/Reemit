using System.Collections.Immutable;
using Reemit.Common;

namespace Reemit.Disassembler.Clr.Disassembler;

public class StreamDisassembler(Stream stream)
{
    private InstructionDecoder _decoder = new(stream);

    public IReadOnlyCollection<RangeMapped<Instruction>> Disassemble()
    {
        var instructions = new List<RangeMapped<Instruction>>();

        while (stream.Position < stream.Length)
        {
            var start = stream.Position;
            var instruction = _decoder.Decode();
            var end = stream.Position;
            var mapped = new RangeMapped<Instruction>((int)start, (int)(end - start), instruction);
            instructions.Add(mapped);
        }

        return instructions.ToImmutableArray();
    }
}
