namespace Reemit.Decompiler.PE.UnitTests;

public static class PETestsHelper
{
    public static ImageDataDirectory EmptyImageDataDirectory { get; }

    static PETestsHelper()
    {
        // virtual address + size
        const int size = sizeof(uint) * 2;
        using var ms = new MemoryStream(new byte[size]);

        EmptyImageDataDirectory = new ImageDataDirectory(new BinaryReader(ms));
    }
}