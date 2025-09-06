using System.Globalization;
using System.Text;
using Reemit.Disassembler.Clr.Metadata.Streams;

namespace Reemit.Disassembler.Clr.Disassembler;

public class OperandEmitter(UserStringsHeapStream userStringsHeapStream)
{
    public void Emit(Operand operand, StringBuilder sb)
    {
        switch (operand.OperandType)
        {
            case OperandType.Int8:
            {
                var value = (sbyte)operand.OperandValue[0];
                sb.Append(value);

                break;
            }

            case OperandType.Int16:
            {
                var value = BitConverter.ToInt16(operand.OperandValue);
                sb.Append(value);

                break;
            }

            case OperandType.Int32:
            {
                var value = BitConverter.ToInt32(operand.OperandValue);
                sb.Append(value);

                break;
            }
            
            case OperandType.Int64:
            {
                var value = BitConverter.ToInt64(operand.OperandValue);
                sb.Append(value);

                break;
            }
            
            case OperandType.UInt8:
            {
                var value = operand.OperandValue[0];
                sb.Append(value);

                break;
            }
            
            case OperandType.UInt16:
            {
                var value = BitConverter.ToUInt16(operand.OperandValue);
                sb.Append(value);

                break;
            }
            
            case OperandType.UInt32:
            {
                var value = BitConverter.ToUInt32(operand.OperandValue);
                sb.Append(value);

                break;
            }
            
            case OperandType.UInt64:
            {
                var value = BitConverter.ToUInt64(operand.OperandValue);
                sb.Append(value);

                break;
            }
            
            case OperandType.Float32:
            {
                var value = BitConverter.ToSingle(operand.OperandValue);
                sb.Append(value.ToString(CultureInfo.InvariantCulture));

                break;
            }
            
            case OperandType.Float64:
            {
                var value = BitConverter.ToDouble(operand.OperandValue);
                sb.Append(value.ToString(CultureInfo.InvariantCulture));

                break;
            }

            case OperandType.MetadataToken:
            {
                var token = MetadataToken.FromByteArray(operand.OperandValue);
                if (!token.IsUserStringHeapRef)
                {
                    sb.Append("MetadataToken { TableRef: ");
                    sb.Append(token.TableRef);
                    sb.Append(", Index: ");
                    sb.Append(token.Index);
                    sb.Append(" }");
                }
                else
                {
                    sb.Append('"');
                    sb.Append(userStringsHeapStream.ReadString(token.Index));
                    sb.Append('"');
                }
                break;
            }
            
            default:
                sb.Append(operand.OperandType);
                sb.Append(" {");

                var operandHex = string.Join(
                    " ",
                    operand.OperandValue.Select(x => $"{x:x2}"));

                sb.Append(operandHex);
                sb.Append('}');

                break;
        }
    }
}