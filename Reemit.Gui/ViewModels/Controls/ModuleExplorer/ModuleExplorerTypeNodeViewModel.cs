using System.Collections.Generic;
using Reemit.Common;
using System.Linq;
using Reemit.Disassembler;
using Reemit.Gui.ViewModels.Navigation;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerTypeNodeViewModel : IModuleExplorerNodeViewModel
{
    public ModuleExplorerTreeViewModel Owner { get; }

    public ClrModule Module { get; }

    public RangeMapped<string> Name { get; }

    public IReadOnlyList<IModuleExplorerNodeViewModel> Children { get; }

    public ModuleExplorerTypeNodeViewModel(ModuleExplorerTreeViewModel owner, ClrModule module, ClrType type)
    {
        Owner = owner;
        Module = module;
        Name = type.Name;
        Children = type.Methods
            .Select(m => new ModuleExplorerMethodNodeViewModel(owner, module, m))
            .ToArray()
            .AsReadOnly();

        NavigationMessageBus.SendMessage(
            NavigationRangeSetterRegistrationMessage.Create(
                this,
                type.Name,
                x => Owner.SelectedNode = x));
    }
}