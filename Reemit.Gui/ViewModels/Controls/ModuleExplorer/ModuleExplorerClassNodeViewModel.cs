using Reemit.Disassembler;

namespace Reemit.Gui.ViewModels.Controls.ModuleExplorer;

public class ModuleExplorerClassNodeViewModel(ModuleExplorerTreeViewModel owner, ClrModule module, ClrType clrType) :
    ModuleExplorerTypeNodeViewModel(owner, module, clrType);