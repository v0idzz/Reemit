using System.Collections.Generic;
using System.Linq;
using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerNamespaceNodeViewModel(ModuleExplorerTreeViewModel owner, ClrModule module, ClrNamespace clrNamespace) : IModuleExplorerNodeViewModel
{
    public ModuleExplorerTreeViewModel Owner => owner;

    public ClrModule Module => module;

    public string Name => clrNamespace.Name;

    public IReadOnlyList<IModuleExplorerNodeViewModel> Children => clrNamespace.Children
        .Select(x => (IModuleExplorerNodeViewModel)
            (x switch
            {
                { IsInterface: true } => new ModuleExplorerInterfaceNodeViewModel(owner, module, x),
                { IsValueType: true } => new ModuleExplorerStructNodeViewModel(owner, module, x),
                _ => new ModuleExplorerClassNodeViewModel(owner, module, x)
            })
        )
        .ToArray()
        .AsReadOnly();

    
}