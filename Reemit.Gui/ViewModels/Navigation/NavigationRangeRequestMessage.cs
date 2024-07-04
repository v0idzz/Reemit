using AvaloniaHex.Document;
using Reemit.Common;

namespace Reemit.Gui.ViewModels.Navigation;

public class NavigationRangeRequestMessage
{
    public BitRange Range { get; }

    public NavigationRangeRequestMessage(BitRange range)
    {
        Range = range;
    }

    public NavigationRangeRequestMessage(IRangeMapped rangeMapped)
        : this(new BitRange((ulong)rangeMapped.Position, (ulong)rangeMapped.End))
    {
    }
}
