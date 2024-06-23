using System.Collections.Generic;
using System.Linq;
using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerNamespaceNodeViewModel(ClrModule module, ClrNamespace clrNamespace) : IModuleExplorerNodeViewModel
{
    public ClrModule Module => module;

    public string Name => clrNamespace.Name;

    public IReadOnlyList<IModuleExplorerNodeViewModel> Children => clrNamespace.Children
        .Select(x => (IModuleExplorerNodeViewModel)
            (x switch
            {
                { IsInterface: true } => new ModuleExplorerInterfaceNodeViewModel(module, x),
                { IsValueType: true } => new ModuleExplorerStructNodeViewModel(module, x),
                _ => new ModuleExplorerClassNodeViewModel(module, x)
            })
        )
        .ToArray()
        .AsReadOnly();

    
}