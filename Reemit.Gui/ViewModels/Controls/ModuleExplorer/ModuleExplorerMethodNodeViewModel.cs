using System.Collections.Generic;
using System.Linq;
using System.Text;
using AvaloniaHex.Document;
using Reemit.Disassembler;
using Reemit.Gui.Common;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerMethodNodeViewModel : IRangeMappedNode
{
    public BitRange Range => Method.MethodBody.ToBitRange();

    public string Name { get; }

    public ModuleExplorerTreeViewModel Owner { get; }

    public ClrModule Module { get; }
    
    public ClrMethod Method { get; }

    public IReadOnlyList<IModuleExplorerNodeViewModel> Children { get; } = [];

    public ModuleExplorerMethodNodeViewModel(ModuleExplorerTreeViewModel owner, ClrModule module, ClrMethod method)
    {
        Owner = owner;
        Module = module;
        Method = method;

        var nameBuilder = new StringBuilder();
        nameBuilder.Append(Method.Name);
        nameBuilder.Append('(');
        nameBuilder.Append(string.Join(", ", Method.Params.Select(p => $"{p.TypeInfo.AliasOrName} {p.Name}")));
        nameBuilder.Append(')');
        nameBuilder.Append(" : ");
        nameBuilder.Append(Method.RetType.AliasOrName);

        Name = nameBuilder.ToString();
    }
}