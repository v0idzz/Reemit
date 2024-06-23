using Reemit.Decompiler;
using System.Collections.Generic;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public interface IModuleExplorerNodeViewModel
{
    public ClrModule Module { get; }

    IReadOnlyList<IModuleExplorerNodeViewModel> Children { get; }
}