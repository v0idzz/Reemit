using System.Collections.Generic;
using System.Linq;
using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerNamespaceNodeViewModel(ClrNamespace clrNamespace) : IModuleExplorerNodeViewModel
{
    public string Name => clrNamespace.Name;

    public IReadOnlyList<IModuleExplorerNodeViewModel> Children => clrNamespace.Children
        .Select(x => (IModuleExplorerNodeViewModel)
            (x switch
            {
                { IsInterface: true } => new ModuleExplorerInterfaceNodeViewModel(x),
                { IsValueType: true } => new ModuleExplorerStructNodeViewModel(x),
                _ => new ModuleExplorerClassNodeViewModel(x)
            })
        )
        .ToArray()
        .AsReadOnly();
}