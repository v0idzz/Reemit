namespace Reemit.Decompiler.PE;

public class CLIHeader : ImageDataDirectoryStructure
{
    public uint Cb { get; private set; }
    public ushort MajorRuntimeVersion { get; private set; }
    public ushort MinorRuntimeVersion { get; private set; }
    public ImageDataDirectory Metadata { get; private set; }
    public RuntimeFlags Flags { get; private set; }
    public uint EntryPointToken { get; private set; }
    public ImageDataDirectory Resources { get; private set; }
    public ImageDataDirectory StrongNameSignature { get; private set; }
    public ImageDataDirectory CodeManagerTable { get; private set; }
    public ImageDataDirectory VTableFixups { get; private set; }
    public ImageDataDirectory ExportAddressTableJumps { get; private set; }
    public ImageDataDirectory ManagedNativeHeader { get; private set; }

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