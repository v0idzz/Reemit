namespace Reemit.Decompiler.PE;

public record ImageDataDirectory
{
    public uint VirtualAddress { get; }
    public uint Size { get; }

    public ImageDataDirectory(BinaryReader reader)
    {
        VirtualAddress = reader.ReadUInt32();
        Size = reader.ReadUInt32();
    }
}