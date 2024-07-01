using Splat;

namespace Reemit.Gui.Common;

public static class MutableDependencyResolverExtension
{
    public static void RegisterConstant<TService, TImplementation>(
        this IMutableDependencyResolver resolver,
        string? contract = null)
        where TImplementation : TService, new() =>
        resolver.RegisterConstant<TService>(new TImplementation(), contract);
}
