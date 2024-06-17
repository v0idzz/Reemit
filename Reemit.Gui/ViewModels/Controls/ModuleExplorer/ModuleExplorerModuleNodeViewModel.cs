using System.Collections.Generic;
using System.Linq;
using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerModuleNodeViewModel(ClrModule clrModule) : IModuleExplorerNodeViewModel
{
    public string Name => clrModule.Name;

    public IReadOnlyCollection<byte> Bytes => clrModule.Bytes;

    public IReadOnlyList<IModuleExplorerNodeViewModel> Children =>
        clrModule.Namespaces.Select(x => new ModuleExplorerNamespaceNodeViewModel(x)).ToArray().AsReadOnly();
}