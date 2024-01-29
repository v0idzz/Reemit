namespace Reemit.Decompiler.PE;

public class PE32PlusWindowsSpecificFields : WindowsSpecificFields<ulong>
{
    public PE32PlusWindowsSpecificFields(BinaryReader reader) : base(reader)
    {
    }
}