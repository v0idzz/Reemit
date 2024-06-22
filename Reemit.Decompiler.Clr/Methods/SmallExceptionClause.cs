namespace Reemit.Decompiler.Clr.Methods;

public class SmallExceptionClause(
    CorILExceptionClauses flags,
    ushort tryOffset,
    byte tryLength,
    ushort handlerOffset,
    byte handlerLength,
    uint classTokenOrFilterOffset)
{
    public CorILExceptionClauses Flags { get; } = flags;
    public ushort TryOffset { get; } = tryOffset;
    public byte TryLength { get; } = tryLength;
    public ushort HandlerOffset { get; } = handlerOffset;
    public byte HandlerLength { get; } = handlerLength;
    public uint ClassTokenOrFilterOffset { get; } = classTokenOrFilterOffset;

    public static SmallExceptionClause Read(BinaryReader reader) =>
        new((CorILExceptionClauses)reader.ReadUInt16(),
            reader.ReadUInt16(),
            reader.ReadByte(),
            reader.ReadUInt16(),
            reader.ReadByte(),
            reader.ReadUInt32());
}