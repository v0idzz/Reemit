namespace Reemit.Disassembler.PE;

public class PE32OptionalHeader : OptionalHeaderBase
{
    public uint BaseOfData { get; }
    public PE32WindowsSpecificFields WindowsSpecificFields { get; }

    public PE32OptionalHeader(BinaryReader binaryReader) : base(binaryReader)
    {
        BaseOfData = binaryReader.ReadUInt32();
        WindowsSpecificFields = new PE32WindowsSpecificFields(binaryReader);
    }
}