    namespace Reemit.Decompiler.Clr.Methods;

public class TinyMethodHeader(uint codeSize) : IMethodHeader
{
    public uint CodeSize { get; } = codeSize;

    public static TinyMethodHeader Read(BinaryReader reader) =>
        new((uint)(reader.ReadByte() & ~((1 << 2) - 1)));
}