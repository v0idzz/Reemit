using Reemit.Common;

namespace Reemit.Decompiler.Clr.Methods;

public static class MethodHeaderReader
{
    public static IMethodHeader ReadMethodHeader(SharedReader reader)
    {
        var tempReader = reader.CreateDerivedAtRelativeToCurrentOffset(0);
        var signatureByte = tempReader.ReadByte();

        var twoLeastSignificantBits = (uint)(signatureByte & 0b11);

        return twoLeastSignificantBits switch
        {
            (uint)CorILMethodFlags.TinyFormat => TinyMethodHeader.Read(reader),
            (uint)CorILMethodFlags.FatFormat => FatMethodHeader.Read(reader),
            _ => throw new BadImageFormatException(
                $"Unrecognized method header (signature: {twoLeastSignificantBits:X})")
        };
    }
}