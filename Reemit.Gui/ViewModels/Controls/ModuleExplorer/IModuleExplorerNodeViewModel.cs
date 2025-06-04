using Reemit.Disassembler;
using System.Collections.Generic;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public interface IModuleExplorerNodeViewModel
{
    public ModuleExplorerTreeViewModel Owner { get; }

    public ClrModule Module { get; }

    IReadOnlyList<IModuleExplorerNodeViewModel> Children { get; }
}