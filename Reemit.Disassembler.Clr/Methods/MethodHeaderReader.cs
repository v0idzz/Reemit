using Reemit.Common;

namespace Reemit.Disassembler.Clr.Methods;

public static class MethodHeaderReader
{
    public static IMethodHeader ReadMethodHeader(SharedReader reader)
    {
        var tempReader = reader.CreateDerivedAtRelativeToCurrentOffset(0);
        var signatureByte = tempReader.ReadByte();

        var format = (CorILMethodFormat)(signatureByte & (byte)CorILMethodFlags.FormatMask);

        return format switch
        {
            CorILMethodFormat.Tiny => TinyMethodHeader.Read(reader),
            CorILMethodFormat.Fat => FatMethodHeader.Read(reader),
            _ => throw new BadImageFormatException(
                $"Unrecognized method header (signature: 0x{format:X})")
        };
    }
}