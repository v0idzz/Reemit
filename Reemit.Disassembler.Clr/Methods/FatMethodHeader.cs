namespace Reemit.Disassembler.Clr.Methods;

public class FatMethodHeader(CorILMethodFlags flags, byte size, ushort maxStack, uint codeSize, uint localVarSigTok)
    : IMethodHeader
{
    public CorILMethodFlags Flags { get; } = flags;
    public byte Size { get; } = size;
    public ushort MaxStack { get; } = maxStack;
    public uint CodeSize { get; } = codeSize;
    public uint LocalVarSigTok { get; } = localVarSigTok;

    public static FatMethodHeader Read(BinaryReader reader)
    {
        var flagsAndSize = reader.ReadUInt16();
        const int flagsMask = (1 << 12) - 1;
        var flags = (CorILMethodFlags)(flagsAndSize & flagsMask);
        var size = (byte)(flagsAndSize >> 12);
        var maxStack = reader.ReadUInt16();
        var codeSize = reader.ReadUInt32();
        var localVarSig = reader.ReadUInt32();

        return new FatMethodHeader(flags, size, maxStack, codeSize, localVarSig);
    }
}