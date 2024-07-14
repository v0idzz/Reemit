using Dock.Model.ReactiveUI.Controls;

namespace Reemit.Gui.ViewModels.Dockables;

public class DockableDocumentViewModel(object viewModel) : Document, IDockableViewModel
{
    public object ViewModel => viewModel;
}