using Reemit.Common;
using System;

namespace Reemit.Gui.ViewModels.Navigation;

public class NavigationRangeDelegateRegistrationMessage(
    IRangeMapped rangeMapped,
    Action navigate,
    Action leave)
    : NavigationRangeRegistrationMessage(rangeMapped)
{
    private Action _navigate = navigate, _leave = leave;

    public override void Navigate() => _navigate();

    public override void Leave() => _leave();
}
