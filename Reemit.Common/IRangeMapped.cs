using System.Diagnostics.CodeAnalysis;

namespace Reemit.Common;

public class RangeMappedComparer : IEqualityComparer<IRangeMapped>
{
    public static RangeMappedComparer Default { get; } = new RangeMappedComparer();    

    public bool Equals(IRangeMapped? x, IRangeMapped? y) =>
        x == null && y == null ?
            true :
        x == null || y == null ?
            false :
            x.Position == y.Position && x.Length == y.Length;

    public int GetHashCode([DisallowNull] IRangeMapped obj)
    {
        var hc = new HashCode();
        hc.Add(obj.Length);
        hc.Add(obj.Position);

        return hc.ToHashCode();
    }
}

public interface IRangeMapped
{
    int Length { get; }
    int Position { get; }
    int End { get; }
}