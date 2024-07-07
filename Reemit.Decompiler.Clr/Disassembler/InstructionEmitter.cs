using System.Text;

namespace Reemit.Decompiler.Clr.Disassembler;

public class InstructionEmitter
{
    public string Emit(IReadOnlyCollection<Instruction> instructions)
    {
        var sb = new StringBuilder();

        foreach (var inst in instructions)
        {
            var mnemonic = MnemonicTable.GetMnemonic(inst.OpcodeInfo);

            sb.Append(mnemonic);

            if (inst.Operand.OperandValue.Any())
            {
                sb.Append(' ');
                sb.Append(inst.Operand.OperandType);
                sb.Append(" {");

                var operandHex = string.Join(
                    " ",
                    inst.Operand.OperandValue.Select(x => string.Format("{0:x2}", x)));

                sb.Append(operandHex);
                sb.Append('}');
            }

            if (!inst.OpcodeInfo.IsPrefix)
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
