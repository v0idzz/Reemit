using System.Collections.Generic;
using Reemit.Common;
using System.Linq;
using AvaloniaHex.Document;
using Reemit.Disassembler;
using Reemit.Gui.Common;
using Reemit.Gui.ViewModels.Navigation;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerTypeNodeViewModel : IRangeMappedNode
{
    public BitRange Range => Name.ToBitRange();

    public ModuleExplorerTreeViewModel Owner { get; }

    public ClrModule Module { get; }

    public RangeMapped<string> Name { get; }

    public IReadOnlyList<IModuleExplorerNodeViewModel> Children { get; }

    public ModuleExplorerTypeNodeViewModel(ModuleExplorerTreeViewModel owner, ClrModule module, ClrType type)
    {
        Owner = owner;
        Module = module;
        Name = type.Name;

        var children = new List<IModuleExplorerNodeViewModel>();

        var implementsInterfacesChildren = type.ImplementsInterfaces
            .Select(i => new ModuleExplorerImplementsInterfaceNodeViewModel(owner, module, i))
            .ToArray()
            .AsReadOnly();

        if (implementsInterfacesChildren.Count != 0)
        {
            var interfacesListNode =
                new ModuleExplorerListNodeViewModel(owner, module,
                    "Implements", implementsInterfacesChildren);

            children.Add(interfacesListNode);
        }

        var typeChildren = type.Methods
            .Select(m => new ModuleExplorerMethodNodeViewModel(owner, module, m));

        children.AddRange(typeChildren);

        Children = children.AsReadOnly();

        NavigationMessageBus.SendMessage(
            NavigationRangeSetterRegistrationMessage.Create(
                this,
                type.Name,
                x => Owner.SelectedNode = x));
    }
}