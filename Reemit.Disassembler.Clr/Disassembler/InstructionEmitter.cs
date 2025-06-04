using System.Text;
using Reemit.Common;

namespace Reemit.Disassembler.Clr.Disassembler;

public class InstructionEmitter
{
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
                sb.Append(inst.Value.Operand.OperandType);
                sb.Append(" {");

                var operandHex = string.Join(
                    " ",
                    inst.Value.Operand.OperandValue.Select(x => string.Format("{0:x2}", x)));

                sb.Append(operandHex);
                sb.Append('}');
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
