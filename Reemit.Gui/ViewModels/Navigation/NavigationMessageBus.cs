using AvaloniaHex.Document;
using ReactiveUI;
using Reemit.Common;
using Reemit.Gui.ViewModels.Controls.HexEditor;
using System;
using System.Reactive.Linq;

namespace Reemit.Gui.ViewModels.Navigation;

public static class NavigationMessageBus
{
    private static MessageBus _bus = new();

    public static IDisposable RegisterMessageSource(IObservable<HexNavigationRangeViewModel> source) =>
        _bus.RegisterMessageSource(source);

    public static IDisposable RegisterMessageSource(IObservable<BitRange> source) =>
        _bus.RegisterMessageSource(source);

    public static IDisposable RegisterMessageSource(IObservable<IRangeMapped> source) =>
        _bus.RegisterMessageSource(source.Select(x => new BitRange((ulong)x.Position, (ulong)x.End)));

    public static IObservable<BitRange> ListenForNavigation() =>
        _bus.Listen<BitRange>();

    public static IObservable<HexNavigationRangeViewModel> ListenForRegistration() =>
        _bus.Listen<HexNavigationRangeViewModel>();

    public static void SendMessage(HexNavigationRangeViewModel navigationRange) =>
        _bus.SendMessage(navigationRange);
}
