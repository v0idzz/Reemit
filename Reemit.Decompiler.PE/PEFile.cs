namespace Reemit.Decompiler.PE;

public class PEFile
{
    public MSDOSHeader MSDOSHeader { get; }
    public COFFHeader CoffHeader { get; }
    public OptionalHeaderBase? OptionalHeader { get; }
    public IReadOnlyCollection<SectionHeader> SectionHeaders { get; }
    
    public PEFile(BinaryReader binaryReader)
    {
        MSDOSHeader = new MSDOSHeader(binaryReader);
        CoffHeader = new COFFHeader(binaryReader);
        
        var optionalHeaderMagic = (OptionalHeaderMagic) binaryReader.ReadUInt16();
        binaryReader.BaseStream.Seek(-sizeof(OptionalHeaderMagic), SeekOrigin.Current);
        
        if (CoffHeader.SizeOfOptionalHeader == 0)
        {
            OptionalHeader = null;
        }
        else
        {
            OptionalHeader = optionalHeaderMagic switch
            {
                OptionalHeaderMagic.PE32 => new PE32OptionalHeader(binaryReader),
                OptionalHeaderMagic.PE32Plus => new PE32PlusOptionalHeader(binaryReader),
                _ => throw new BadImageFormatException("Unrecognized Optional Header Magic value in PE header.")
            };
        }
        
        var sectionHeaders = new SectionHeader[CoffHeader.NumberOfSections];

        for (var i = 0; i < CoffHeader.NumberOfSections; i++)
        {
            sectionHeaders[i] = new SectionHeader(binaryReader);
        }

        SectionHeaders = sectionHeaders.AsReadOnly();
    }
}