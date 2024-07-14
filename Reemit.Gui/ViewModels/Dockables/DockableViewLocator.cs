using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Reemit.Gui.Views.Controls.Dockables;

namespace Reemit.Gui.ViewModels.Dockables;

public class DockableViewLocator : IDataTemplate
{
    public Control Build(object? param) => new DockableWrapperView();

    public bool Match(object? data) => data is DockableToolViewModel or DockableDocumentViewModel;
}