using Reemit.Common;
using Reemit.Decompiler.PE.Enums;

namespace Reemit.Decompiler.PE;

public class SectionHeader
{
    public string Name { get; }
    public uint VirtualSize { get; }
    public uint VirtualAddress { get; }
    public uint SizeOfRawData { get; }
    public uint PointerToRawData { get; }
    public uint PointerToRelocations { get; }
    public uint PointerToLinenumbers { get; }
    public uint NumberOfRelocations { get; }
    public uint NumberOfLinenumbers { get; }
    public SectionFlags Characteristics { get; }

    public SectionHeader(BinaryReader reader)
    {
        Name = reader.ReadUtf8String(8).TrimEnd('\0');
        VirtualSize = reader.ReadUInt32();
        VirtualAddress = reader.ReadUInt32();
        SizeOfRawData = reader.ReadUInt32();
        PointerToRawData = reader.ReadUInt32();
        PointerToRelocations = reader.ReadUInt32();
        PointerToLinenumbers = reader.ReadUInt32();
        NumberOfRelocations = reader.ReadUInt16();
        NumberOfLinenumbers = reader.ReadUInt16();
        Characteristics = (SectionFlags)reader.ReadUInt32();
    }
}