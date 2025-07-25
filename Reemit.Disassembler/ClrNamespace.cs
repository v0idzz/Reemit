namespace Reemit.Disassembler;

public class ClrNamespace(string name, IReadOnlyList<ClrType> children)
{
    public string Name => name;

    public IReadOnlyList<ClrType> Children => children;
}