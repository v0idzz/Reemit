using AvaloniaHex.Document;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public interface IRangeMappedNode : IModuleExplorerNodeViewModel
{
    BitRange Range { get; }
}