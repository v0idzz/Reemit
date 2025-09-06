using System.Text;
using Reemit.Common;
using Reemit.Disassembler.Clr.Metadata.Streams;

namespace Reemit.Disassembler.Clr.Disassembler;

public class InstructionEmitter(UserStringsHeapStream userStringsHeapStream)
{
    private readonly OperandEmitter _operandEmitter = new(userStringsHeapStream);
    
    public string Emit(IReadOnlyCollection<RangeMapped<Instruction>> instructions)
    {
        var sb = new StringBuilder();

        foreach (var inst in instructions)
        {
            var mnemonic = MnemonicTable.GetMnemonic(inst.Value.OpcodeInfo);

            sb.Append(mnemonic);

            if (inst.Value.Operand.OperandValue.Any())
            {
                sb.Append(' ');
                
                _operandEmitter.Emit(inst.Value.Operand, sb);
            }

            if (!inst.Value.OpcodeInfo.IsPrefix)
            {
                sb.Append(Environment.NewLine);
            }
            else
            {
                sb.Append(' ');
            }
        }

        return sb.ToString();
    }
}
