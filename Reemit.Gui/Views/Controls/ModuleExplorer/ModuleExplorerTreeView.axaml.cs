using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reemit.Gui.ViewModels.Controls.ModuleExplorer;

namespace Reemit.Gui.Views.Controls.ModuleExplorer;

public partial class ModuleExplorerTreeView : ReactiveUserControl<ModuleExplorerTreeViewModel>
{
    public ModuleExplorerTreeView()
    {
        InitializeComponent();
        this.WhenActivated(disposable =>
        {
            this.OneWayBind(ViewModel, vm => vm.RootNodes, v => v.TreeView.ItemsSource)
                .DisposeWith(disposable);
        });
    }
}