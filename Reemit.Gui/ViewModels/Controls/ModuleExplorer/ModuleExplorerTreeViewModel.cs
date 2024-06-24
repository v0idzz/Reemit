using System;
using System.Collections.ObjectModel;
using DynamicData;
using DynamicData.Alias;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerTreeViewModel : ReactiveObject
{
    public ReadOnlyObservableCollection<ModuleExplorerModuleNodeViewModel> RootNodes => _rootNodes;
    private readonly ReadOnlyObservableCollection<ModuleExplorerModuleNodeViewModel> _rootNodes;

    [Reactive]
    public IModuleExplorerNodeViewModel? SelectedNode { get; set; }

    public ModuleExplorerTreeViewModel(ReadOnlyObservableCollection<ClrModule> clrModules)
    {
        clrModules.ToObservableChangeSet()
            .Select(x => new ModuleExplorerModuleNodeViewModel(this, x))
            .Bind(out _rootNodes)
            .Subscribe();
    }
}