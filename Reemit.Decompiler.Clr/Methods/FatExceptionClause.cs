namespace Reemit.Decompiler.Clr.Methods;

public class FatExceptionClause(
    CorILExceptionClauses flags,
    uint tryOffset,
    uint tryLength,
    uint handlerOffset,
    uint handlerLength,
    uint classTokenOrFilterOffset)
{
    public CorILExceptionClauses Flags { get; } = flags;
    public uint TryOffset { get; } = tryOffset;
    public uint TryLength { get; } = tryLength;
    public uint HandlerOffset { get; } = handlerOffset;
    public uint HandlerLength { get; } = handlerLength;
    public uint ClassTokenOrFilterOffset { get; } = classTokenOrFilterOffset;

    public static FatExceptionClause Read(BinaryReader reader) =>
        new((CorILExceptionClauses)reader.ReadUInt32(),
            reader.ReadUInt32(),
            reader.ReadUInt32(),
            reader.ReadUInt32(),
            reader.ReadUInt32(),
            reader.ReadUInt32());
}