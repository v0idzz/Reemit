using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerStructNodeViewModel(ClrModule module, ClrType clrType) : ModuleExplorerTypeNodeViewModel(module, clrType);