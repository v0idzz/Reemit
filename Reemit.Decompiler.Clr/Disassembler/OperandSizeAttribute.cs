namespace Reemit.Decompiler.Clr.Disassembler;

[AttributeUsage(AttributeTargets.Field)]
public class OperandSizeAttribute : Attribute
{
    public int SizeInBits { get; }

    public int SizeInBytes => SizeInBits * 8;

    public bool IsFixedLength { get; }

    public OperandSizeAttribute(int sizeInBits)
        : this(isFixedLength: true, sizeInBits)
    {
    }

    public OperandSizeAttribute(bool isFixedLength, int sizeInBits = 0)
    {
        SizeInBits = sizeInBits;
        IsFixedLength = isFixedLength;
    }
}
