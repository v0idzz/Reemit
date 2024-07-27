using Dock.Model.ReactiveUI.Controls;

namespace Reemit.Gui.ViewModels.Dockables;

public class DockableToolViewModel(object viewModel) : Tool, IDockableViewModel
{
    public object ViewModel => viewModel;
}