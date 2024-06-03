using System.Collections.Generic;
using ReactiveUI;
using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels;

public class HomeViewModel : ReactiveObject, IRoutableViewModel
{
    public IReadOnlyList<ClrModule> Modules { get; }

    public HomeViewModel(IScreen hostScreen, IReadOnlyList<ClrModule> modules)
    {
        Modules = modules;
        HostScreen = hostScreen;
    }

    public string UrlPathSegment => nameof(HomeViewModel);
    public IScreen HostScreen { get; }
}