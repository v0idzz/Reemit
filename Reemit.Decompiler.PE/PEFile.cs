using Reemit.Decompiler.PE.Enums;

namespace Reemit.Decompiler.PE;

public class PEFile
{
    public MSDosHeader MSDosHeader { get; }
    public CoffHeader CoffHeader { get; }
    public OptionalHeaderBase? OptionalHeader { get; }
    public IReadOnlyCollection<SectionHeader> SectionHeaders { get; }
    public IReadOnlyCollection<ImageDataDirectory> DataDirectories { get; }

    private readonly BinaryReader _binaryReader;

    private readonly object _readLock = new();
    
    public PEFile(BinaryReader binaryReader)
    {
        _binaryReader = binaryReader;

        MSDosHeader = new MSDosHeader(binaryReader);
        CoffHeader = new CoffHeader(binaryReader);
        
        var optionalHeaderMagic = (OptionalHeaderMagic) binaryReader.ReadUInt16();
        binaryReader.BaseStream.Seek(-sizeof(OptionalHeaderMagic), SeekOrigin.Current);
        
        if (CoffHeader.SizeOfOptionalHeader == 0)
        {
            OptionalHeader = null;
        }
        else
        {
            OptionalHeaderBase optionalHeader = optionalHeaderMagic switch
            {
                OptionalHeaderMagic.PE32 => new PE32OptionalHeader(binaryReader),
                OptionalHeaderMagic.PE32Plus => new PE32PlusOptionalHeader(binaryReader),
                _ => throw new BadImageFormatException("Unrecognized Optional Header Magic value in PE header.")
            };

            OptionalHeader = optionalHeader;
        }
        
        var sectionHeaders = new SectionHeader[CoffHeader.NumberOfSections];

        for (var i = 0; i < CoffHeader.NumberOfSections; i++)
        {
            sectionHeaders[i] = new SectionHeader(binaryReader);
        }

        SectionHeaders = sectionHeaders.AsReadOnly();
        
        DataDirectories = OptionalHeader switch
        {
            PE32OptionalHeader h => h.WindowsSpecificFields.DataDirectories,
            PE32PlusOptionalHeader h => h.WindowsSpecificFields.DataDirectories,
            _ => throw new BadImageFormatException("Unrecognized Optional Header Magic value in PE header.")
        };
    }

    public uint GetFileOffset(uint rva)
    {
        var sectionHeader = SectionHeaders.SingleOrDefault(x => rva >= x.VirtualAddress && rva < x.VirtualAddress + x.VirtualSize);
        if (sectionHeader == null)
        {
            throw new ArgumentOutOfRangeException(nameof(rva), "RVA is not contained in any section.");
        }
        
        return sectionHeader.PointerToRawData + (rva - sectionHeader.VirtualAddress);
    }

    public T GetStructureDescribedByDataDirectory<T>(ImageDataDirectory directory)
        where T : ImageDataDirectoryStructure, new()
    {
        var fileOffset = GetFileOffset(directory.VirtualAddress);
        var structure = new T();

        lock (_readLock)
        {
            _binaryReader.BaseStream.Seek(fileOffset, SeekOrigin.Begin);
            structure.Read(_binaryReader);
        }

        return structure;
    }
}