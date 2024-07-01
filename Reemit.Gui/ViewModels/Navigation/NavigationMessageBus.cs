using ReactiveUI;
using System;

namespace Reemit.Gui.ViewModels.Navigation;

public static class NavigationMessageBus
{
    private static MessageBus _bus = new();

    public static IDisposable RegisterMessageSource(IObservable<NavigationRangeRequestMessage> source) =>
        _bus.RegisterMessageSource(source);

    public static IObservable<NavigationRangeRequestMessage> ListenForNavigation() =>
        _bus.Listen<NavigationRangeRequestMessage>();

    public static IObservable<NavigationRangeRegistrationMessage> ListenForRegistration() =>
        _bus.Listen<NavigationRangeRegistrationMessage>();

    public static void SendMessage(NavigationRangeRegistrationMessage navigationRange) =>
        _bus.SendMessage(navigationRange);
}
