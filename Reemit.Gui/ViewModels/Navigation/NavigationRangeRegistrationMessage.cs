using Reemit.Common;

namespace Reemit.Gui.ViewModels.Navigation;

public abstract class NavigationRangeRegistrationMessage(IRangeMapped rangeMapped)
{
    public IRangeMapped RangeMapped { get; } = rangeMapped;

    public abstract void Navigate();

    public abstract void Leave();
}
