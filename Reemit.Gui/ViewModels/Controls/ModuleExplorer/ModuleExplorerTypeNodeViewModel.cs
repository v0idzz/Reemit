using System.Collections.Generic;
using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerTypeNodeViewModel(ClrType type) : IModuleExplorerNodeViewModel
{
    public string Name => type.Name;

    public IReadOnlyList<IModuleExplorerNodeViewModel> Children => [];
}