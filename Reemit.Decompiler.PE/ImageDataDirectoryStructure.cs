namespace Reemit.Decompiler.PE;

public abstract class ImageDataDirectoryStructure
{
    internal abstract void Read(BinaryReader reader);
}