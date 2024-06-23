using System.Collections.Generic;
using Reemit.Common;
using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerTypeNodeViewModel(ClrModule module, ClrType type) : IModuleExplorerNodeViewModel
{
    public ClrModule Module => module;

    public RangeMapped<string> Name => type.Name;

    public IReadOnlyList<IModuleExplorerNodeViewModel> Children => [];
}