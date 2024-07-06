using System.Collections.Generic;
using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerMethodNodeViewModel(ModuleExplorerTreeViewModel owner, ClrModule module, ClrMethod method)
    : IModuleExplorerNodeViewModel
{
    public string Name => method.Name;

    public ModuleExplorerTreeViewModel Owner => owner;

    public ClrModule Module => module;

    public IReadOnlyList<IModuleExplorerNodeViewModel> Children { get; } = [];
}