namespace Reemit.Decompiler.Clr.Methods;

public class SmallExceptionHeader(
    CorILMethodSectionFlags kind,
    byte dataSize,
    ushort reserved,
    IReadOnlyList<SmallExceptionClause> clauses) : IMethodDataSection
{
    public CorILMethodSectionFlags Kind { get; } = kind;
    public byte DataSize { get; } = dataSize;
    public ushort Reserved { get; } = reserved;
    public IReadOnlyList<SmallExceptionClause> Clauses { get; } = clauses;

    public static SmallExceptionHeader Read(BinaryReader reader)
    {
        var kind = (CorILMethodSectionFlags)reader.ReadByte();
        var dataSize = reader.ReadByte();
        var reserved = reader.ReadUInt16();

        if (reserved != 0)
        {
            throw new BadImageFormatException("Padding should always be 0");
        }

        // From ECMA-335 II.25.4.5
        const int exceptionClauseSize = 12;
        const int headerSize = 4;

        var clausesCount = (dataSize - headerSize) / exceptionClauseSize;
        var clauses = new List<SmallExceptionClause>(clausesCount);

        for (var i = 0; i < clausesCount; i++)
        {
            clauses.Add(SmallExceptionClause.Read(reader));
        }

        return new SmallExceptionHeader(kind, dataSize, reserved, clauses);
    }
}