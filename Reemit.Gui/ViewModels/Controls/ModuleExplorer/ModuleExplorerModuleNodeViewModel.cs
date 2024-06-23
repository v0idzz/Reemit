using System.Collections.Generic;
using System.Linq;
using Reemit.Common;
using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerModuleNodeViewModel(ClrModule clrModule) : IModuleExplorerNodeViewModel
{
    public ClrModule Module => clrModule;

    public RangeMapped<string> Name => clrModule.Name;

    public IReadOnlyList<IModuleExplorerNodeViewModel> Children =>
        clrModule.Namespaces.Select(x => new ModuleExplorerNamespaceNodeViewModel(clrModule, x)).ToArray().AsReadOnly();

    
}