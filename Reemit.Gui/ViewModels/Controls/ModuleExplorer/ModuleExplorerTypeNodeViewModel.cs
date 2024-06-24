using System.Collections.Generic;
using Reemit.Common;
using Reemit.Decompiler;
using Reemit.Gui.ViewModels.Navigation;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerTypeNodeViewModel : IModuleExplorerNodeViewModel
{
    public ModuleExplorerTreeViewModel Owner { get;  }

    public ClrModule Module { get; }

    public RangeMapped<string> Name { get; }

    public IReadOnlyList<IModuleExplorerNodeViewModel> Children => [];

    public ModuleExplorerTypeNodeViewModel(ModuleExplorerTreeViewModel owner, ClrModule module, ClrType type)
    {
        Owner = owner;
        Module = module;
        Name = type.Name;

        NavigationMessageBus.SendMessage(
            new HexEditor.HexNavigationRangeViewModel(
                type.Name,
                () => Owner.SelectedNode = this));
    }
}