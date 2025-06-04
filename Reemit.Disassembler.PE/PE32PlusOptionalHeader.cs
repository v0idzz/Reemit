namespace Reemit.Disassembler.PE;

public class PE32PlusOptionalHeader : OptionalHeaderBase
{
    public PE32PlusWindowsSpecificFields WindowsSpecificFields { get; }

    public PE32PlusOptionalHeader(BinaryReader binaryReader) : base(binaryReader)
    {
        WindowsSpecificFields = new PE32PlusWindowsSpecificFields(binaryReader);
    }
}