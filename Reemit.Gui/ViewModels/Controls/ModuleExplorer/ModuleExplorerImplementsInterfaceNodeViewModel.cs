using System.Collections.Generic;
using Reemit.Disassembler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerImplementsInterfaceNodeViewModel(
    ModuleExplorerTreeViewModel owner,
    ClrModule module,
    ClrTypeInfo @interface)
    : IModuleExplorerNodeViewModel
{
    public string Name { get; } = @interface.AliasOrName;
    public ModuleExplorerTreeViewModel Owner { get; } = owner;
    public ClrModule Module { get; } = module;
    public ClrTypeInfo Interface { get; } = @interface;
    public IReadOnlyList<IModuleExplorerNodeViewModel> Children { get; } = [];
}