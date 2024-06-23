using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using AvaloniaHex.Document;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reemit.Decompiler;
using Reemit.Gui.ViewModels.Controls.HexEditor;
using Reemit.Gui.ViewModels.Controls.ModuleExplorer;

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
            .BindTo(HexEditorViewModel, x => x.ModuleBytes);

        this.WhenAnyValue(x => x.ModuleExplorerTreeViewModel.SelectedNode)
            .Select(x => x as ModuleExplorerTypeNodeViewModel)
            .WhereNotNull()
            .Select(x => x.Name)
            .Select(x => new BitRange((ulong)x.Position, (ulong)x.End))
            .BindTo(HexEditorViewModel, x => x.SelectedRange);
    }

    public string UrlPathSegment => nameof(HomeViewModel);
    public IScreen HostScreen { get; }
}