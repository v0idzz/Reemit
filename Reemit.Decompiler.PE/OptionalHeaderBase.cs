namespace Reemit.Decompiler.PE;

public abstract class OptionalHeaderBase
{
    public OptionalHeaderMagic Magic { get; }
    public byte MajorLinkerVersion { get; }
    public byte MinorLinkerVersion { get; }
    public uint SizeOfCode { get; }
    public uint SizeOfInitializedData { get; }
    public uint SizeOfUninitializedData { get; }
    public uint AddressOfEntryPoint { get; }
    public uint BaseOfCode { get; }

    protected OptionalHeaderBase(BinaryReader binaryReader)
    {
        Magic = (OptionalHeaderMagic)binaryReader.ReadUInt16();
        MajorLinkerVersion = binaryReader.ReadByte();
        MinorLinkerVersion = binaryReader.ReadByte();
        SizeOfCode = binaryReader.ReadUInt32();
        SizeOfInitializedData = binaryReader.ReadUInt32();
        SizeOfUninitializedData = binaryReader.ReadUInt32();
        AddressOfEntryPoint = binaryReader.ReadUInt32();
        BaseOfCode = binaryReader.ReadUInt32();
    }
}