using System;
using System.Collections.ObjectModel;
using DynamicData;
using DynamicData.Alias;
using DynamicData.Binding;
using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerTreeViewModel
{
    public ReadOnlyObservableCollection<ModuleExplorerModuleNodeViewModel> RootNodes => _rootNodes;
    private readonly ReadOnlyObservableCollection<ModuleExplorerModuleNodeViewModel> _rootNodes;

    public ModuleExplorerTreeViewModel(ReadOnlyObservableCollection<ClrModule> clrModules)
    {
        clrModules.ToObservableChangeSet()
            .Select(x => new ModuleExplorerModuleNodeViewModel(x))
            .Bind(out _rootNodes)
            .Subscribe();
    }
}