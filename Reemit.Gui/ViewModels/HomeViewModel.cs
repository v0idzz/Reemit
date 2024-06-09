using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData;
using ReactiveUI;
using Reemit.Decompiler;
using Reemit.Gui.ViewModels.Controls.ModuleExplorer;

namespace Reemit.Gui.ViewModels;

public class HomeViewModel : ReactiveObject, IRoutableViewModel
{
    private readonly ReadOnlyObservableCollection<ClrModule> _modules;
    
    public ModuleExplorerTreeViewModel ModuleExplorerTreeViewModel { get; }

    private readonly SourceList<ClrModule> _modulesSource;

    public HomeViewModel(IScreen hostScreen, IReadOnlyList<ClrModule> modules)
    {
        _modulesSource = new SourceList<ClrModule>();
        _modulesSource.AddRange(modules);
        _modulesSource.Connect()
            .Bind(out _modules)
            .Subscribe();

        HostScreen = hostScreen;
        ModuleExplorerTreeViewModel = new ModuleExplorerTreeViewModel(_modules);
    }

    public string UrlPathSegment => nameof(HomeViewModel);
    public IScreen HostScreen { get; }
}