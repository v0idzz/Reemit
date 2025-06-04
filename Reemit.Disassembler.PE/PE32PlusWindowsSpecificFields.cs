namespace Reemit.Disassembler.PE;

public class PE32PlusWindowsSpecificFields : WindowsSpecificFields<ulong>
{
    public PE32PlusWindowsSpecificFields(BinaryReader reader) : base(reader)
    {
    }
}