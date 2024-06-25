using Reemit.Common;
using System;

namespace Reemit.Gui.ViewModels.Navigation;

public class NavigationRangeSetterRegistrationMessage<TSource>(
    TSource source,
    IRangeMapped rangeMapped,
    Action<TSource?> setter) : NavigationRangeRegistrationMessage(rangeMapped)
    where TSource : class
{
    public override void Navigate() => setter(source);

    public override void Leave() => setter(null);
}

public static class NavigationRangeSetterRegistrationMessage
{
    public static NavigationRangeSetterRegistrationMessage<TSource> Create<TSource>(
        TSource source,
        IRangeMapped rangeMapped,
        Action<TSource?> setter)
        where TSource : class =>
        new NavigationRangeSetterRegistrationMessage<TSource>(source, rangeMapped, setter);
}
