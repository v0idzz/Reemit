using ReactiveUI;
using System;

namespace Reemit.Gui.ViewModels.Navigation;

public static class NavigationMessageBus
{
    private static MessageBus _bus = new();

    public static IDisposable RegisterMessageSource(IObservable<NavigationRequestMessage> source) =>
        _bus.RegisterMessageSource(source);

    public static IObservable<NavigationRequestMessage> ListenForNavigation() =>
        _bus.Listen<NavigationRequestMessage>();

    public static IObservable<NavigationRangeRegistrationMessage> ListenForRegistration() =>
        _bus.Listen<NavigationRangeRegistrationMessage>();

    public static void SendMessage(NavigationRangeRegistrationMessage navigationRange) =>
        _bus.SendMessage(navigationRange);
}
