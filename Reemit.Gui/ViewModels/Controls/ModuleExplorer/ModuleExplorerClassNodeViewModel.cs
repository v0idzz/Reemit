using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerClassNodeViewModel(ClrModule module, ClrType clrType) : ModuleExplorerTypeNodeViewModel(module, clrType);