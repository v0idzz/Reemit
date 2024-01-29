using Reemit.Common;

namespace Reemit.Decompiler.PE;

public abstract class WindowsSpecificFields<T>
    where T : unmanaged
{
    public T ImageBase { get; }
    public uint SectionAlignment { get; }
    public uint FileAlignment { get; }
    public ushort MajorOperatingSystemVersion { get; }
    public ushort MinorOperatingSystemVersion { get; }
    public ushort MajorImageVersion { get; }
    public ushort MinorImageVersion { get; }
    public ushort MajorSubsystemVersion { get; }
    public ushort MinorSubsystemVersion { get; }
    public uint Win32VersionValue { get; }
    public uint SizeOfImage { get; }
    public uint SizeOfHeaders { get; }
    public uint CheckSum { get; }
    public WindowsSubsystem Subsystem { get; }
    public DllCharacteristics DllCharacteristics { get; }
    public T SizeOfStackReserve { get; }
    public T SizeOfStackCommit { get; }
    public T SizeOfHeapReserve { get; }
    public T SizeOfHeapCommit { get; }
    public uint LoaderFlags { get; }
    public uint NumberOfRvaAndSizes { get; }
    public IReadOnlyCollection<ImageDataDirectory> DataDirectories { get; }

    internal WindowsSpecificFields(BinaryReader reader)
    {
        ImageBase = reader.ReadStruct<T>();
        SectionAlignment = reader.ReadUInt32();
        FileAlignment = reader.ReadUInt32();
        MajorOperatingSystemVersion = reader.ReadUInt16();
        MinorOperatingSystemVersion = reader.ReadUInt16();
        MajorImageVersion = reader.ReadUInt16();
        MinorImageVersion = reader.ReadUInt16();
        MajorSubsystemVersion = reader.ReadUInt16();
        MinorSubsystemVersion = reader.ReadUInt16();
        Win32VersionValue = reader.ReadUInt32();
        SizeOfImage = reader.ReadUInt32();
        SizeOfHeaders = reader.ReadUInt32();
        CheckSum = reader.ReadUInt32();
        Subsystem = (WindowsSubsystem)reader.ReadUInt16();
        DllCharacteristics = (DllCharacteristics)reader.ReadUInt16();
        SizeOfStackReserve = reader.ReadStruct<T>();
        SizeOfStackCommit = reader.ReadStruct<T>();
        SizeOfHeapReserve = reader.ReadStruct<T>();
        SizeOfHeapCommit = reader.ReadStruct<T>();
        LoaderFlags = reader.ReadUInt32();
        NumberOfRvaAndSizes = reader.ReadUInt32();
        
        var dataDirectories = new ImageDataDirectory[NumberOfRvaAndSizes];

        for (var i = 0; i < NumberOfRvaAndSizes; i++)
        {
            dataDirectories[i] = new ImageDataDirectory(reader);
        }

        DataDirectories = dataDirectories.AsReadOnly();
    }
}