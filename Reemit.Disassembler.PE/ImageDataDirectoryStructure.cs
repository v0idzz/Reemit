namespace Reemit.Disassembler.PE;

public abstract class ImageDataDirectoryStructure
{
    internal abstract void Read(BinaryReader reader);
}