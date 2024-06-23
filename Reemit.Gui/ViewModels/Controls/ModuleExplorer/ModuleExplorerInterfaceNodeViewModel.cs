using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerInterfaceNodeViewModel(ClrModule module, ClrType type) : ModuleExplorerTypeNodeViewModel(module, type);