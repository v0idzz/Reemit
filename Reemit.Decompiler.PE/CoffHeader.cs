using Reemit.Common;

namespace Reemit.Decompiler.PE;

public class CoffHeader
{
    public string Signature { get; }
    public MachineType Machine { get; }
    public short NumberOfSections { get; }
    public uint TimeDateStamp { get; }
    public int PointerToSymbolTable { get; }
    public int NumberOfSymbols { get; }
    public short SizeOfOptionalHeader { get; }
    public PECharacteristics Characteristics { get; }
    
    public const string SignatureValue = "PE\0\0";

    public CoffHeader(BinaryReader reader)
    {
        Signature = reader.ReadAsciiString(4);

        if (Signature != SignatureValue)
            throw new BadImageFormatException("Invalid Signature value in PE header.");

        Machine = (MachineType)reader.ReadUInt16();
        NumberOfSections = reader.ReadInt16();
        TimeDateStamp = reader.ReadUInt32();
        PointerToSymbolTable = reader.ReadInt32();
        NumberOfSymbols = reader.ReadInt32();
        SizeOfOptionalHeader = reader.ReadInt16();
        Characteristics = (PECharacteristics)reader.ReadUInt16();
    }
}