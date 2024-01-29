namespace Reemit.Decompiler.PE;

public class PE32WindowsSpecificFields : WindowsSpecificFields<uint>
{
    public PE32WindowsSpecificFields(BinaryReader reader) : base(reader)
    {
    }
}