namespace Reemit.Disassembler.Clr.Methods;

public class FatExceptionHeader(
    CorILMethodSectionFlags kind,
    uint dataSize,
    IReadOnlyList<FatExceptionClause> clauses) : IMethodDataSection
{
    public CorILMethodSectionFlags Kind { get; } = kind;
    public uint DataSize { get; } = dataSize;
    public IReadOnlyList<FatExceptionClause> Clauses { get; } = clauses;

    public static FatExceptionHeader Read(BinaryReader reader)
    {
        var kind = (CorILMethodSectionFlags)reader.ReadByte();
        var dataSize = BitConverter.ToUInt32([..reader.ReadBytes(3), 0]);
        
        // From ECMA-335 II.25.4.5
        const int exceptionClauseSize = 24;
        const int headerSize = 4;

        var clausesCount = (int)((dataSize - headerSize) / exceptionClauseSize);
        var clauses = new List<FatExceptionClause>(clausesCount);

        for (var i = 0; i < clausesCount; i++)
        {
            clauses.Add(FatExceptionClause.Read(reader));
        }

        return new FatExceptionHeader(kind, dataSize, clauses);
    }
}