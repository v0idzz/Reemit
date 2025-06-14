using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reemit.Gui.ViewModels.Controls.ModuleExplorer;

namespace Reemit.Gui.Views.Controls.ModuleExplorer;

public partial class ModuleExplorerListNodeView : ReactiveUserControl<ModuleExplorerListNodeViewModel>
{
    public ModuleExplorerListNodeView()
    {
        InitializeComponent();
        this.WhenActivated(disposable =>
        {
            this.OneWayBind(ViewModel, vm => vm.Title, v => v.NodeView.Text)
                .DisposeWith(disposable);
        });
    }
}