using AvaloniaHex.Document;
using Reemit.Common;
using Reemit.Gui.ViewModels.Controls.ModuleExplorer;

namespace Reemit.Gui.ViewModels.Navigation;

public class NavigationRequestMessage
{
    public IRangeMappedNode SelectedNode { get; set; }

    public BitRange Range { get; }

    public NavigationRequestMessage(BitRange range, IRangeMappedNode selectedNode)
    {
        Range = range;
        SelectedNode = selectedNode;
    }

    public NavigationRequestMessage(IRangeMapped rangeMapped, IRangeMappedNode selectedNode)
        : this(new BitRange((ulong)rangeMapped.Position, (ulong)rangeMapped.End), selectedNode)
    {
    }
}
