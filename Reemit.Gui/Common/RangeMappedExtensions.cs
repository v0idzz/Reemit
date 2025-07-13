using AvaloniaHex.Document;
using Reemit.Common;

namespace Reemit.Gui.Common;

public static class RangeMappedExtensions
{
    public static BitRange ToBitRange<T>(this RangeMapped<T> rangeMapped) =>
        new((ulong)rangeMapped.Position, (ulong)rangeMapped.End);
}