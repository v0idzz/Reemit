using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerInterfaceNodeViewModel(ModuleExplorerTreeViewModel owner, ClrModule module, ClrType type)
    : ModuleExplorerTypeNodeViewModel(owner, module, type);