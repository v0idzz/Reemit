using System.Collections.Generic;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public interface IModuleExplorerNodeViewModel
{
    IReadOnlyList<IModuleExplorerNodeViewModel> Children { get; }
}