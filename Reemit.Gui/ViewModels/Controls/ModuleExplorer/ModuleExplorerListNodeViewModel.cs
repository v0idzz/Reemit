using System.Collections.Generic;
using Reemit.Disassembler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerListNodeViewModel(
    ModuleExplorerTreeViewModel owner,
    ClrModule module,
    string title,
    IReadOnlyList<IModuleExplorerNodeViewModel> children)
    : IModuleExplorerNodeViewModel
{
    public ModuleExplorerTreeViewModel Owner => owner;
    public ClrModule Module => module;
    public string Title => title;
    public IReadOnlyList<IModuleExplorerNodeViewModel> Children => children;
}