using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using Reemit.Decompiler;
using Reemit.Gui.ViewModels.Controls.HexEditor;
using Reemit.Gui.ViewModels.Controls.ModuleExplorer;
using Reemit.Gui.ViewModels.Navigation;

namespace Reemit.Gui.ViewModels;

public class HomeViewModel : ReactiveObject, IRoutableViewModel
{
    private readonly ReadOnlyObservableCollection<ClrModule> _modules;
    
    public ModuleExplorerTreeViewModel ModuleExplorerTreeViewModel { get; }
    public HexEditorViewModel HexEditorViewModel { get; }

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
        HexEditorViewModel = new HexEditorViewModel();

        this.WhenAnyValue(x => x.ModuleExplorerTreeViewModel.SelectedNode)
            .Select(x => x?.Module.Bytes)
            .WhereNotNull()
            .BindTo(HexEditorViewModel, x => x.ModuleBytes);

        NavigationMessageBus.RegisterMessageSource(
            this.WhenAnyValue(x => x.ModuleExplorerTreeViewModel.SelectedNode)
                .Select(x => x as ModuleExplorerTypeNodeViewModel)
                .WhereNotNull()
                .Select(x => new NavigationRangeRequestMessage(x.Name)));
    }

    public string UrlPathSegment => nameof(HomeViewModel);
    public IScreen HostScreen { get; }
}