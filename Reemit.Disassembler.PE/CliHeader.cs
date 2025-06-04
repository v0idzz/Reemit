using Reemit.Disassembler.PE.Enums;

namespace Reemit.Disassembler.PE;

public class CliHeader : ImageDataDirectoryStructure
{
    public uint Cb { get; private set; }
    public ushort MajorRuntimeVersion { get; private set; }
    public ushort MinorRuntimeVersion { get; private set; }
    public ImageDataDirectory Metadata { get; private set; } = null!;
    public RuntimeFlags Flags { get; private set; }
    public uint EntryPointToken { get; private set; }
    public ImageDataDirectory Resources { get; private set; } = null!;
    public ImageDataDirectory StrongNameSignature { get; private set; } = null!;
    public ImageDataDirectory CodeManagerTable { get; private set; } = null!;
    public ImageDataDirectory VTableFixups { get; private set; } = null!;
    public ImageDataDirectory ExportAddressTableJumps { get; private set; } = null!;
    public ImageDataDirectory ManagedNativeHeader { get; private set; } = null!;

    internal override void Read(BinaryReader reader)
    {
        Cb = reader.ReadUInt32();
        MajorRuntimeVersion = reader.ReadUInt16();
        MinorRuntimeVersion = reader.ReadUInt16();
        Metadata = new ImageDataDirectory(reader);
        Flags = (RuntimeFlags)reader.ReadUInt32();
        EntryPointToken = reader.ReadUInt32();
        Resources = new ImageDataDirectory(reader);
        StrongNameSignature = new ImageDataDirectory(reader);
        CodeManagerTable = new ImageDataDirectory(reader);
        VTableFixups = new ImageDataDirectory(reader);
        ExportAddressTableJumps = new ImageDataDirectory(reader);
        ManagedNativeHeader = new ImageDataDirectory(reader);
    }
}