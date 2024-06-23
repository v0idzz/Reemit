using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reemit.Gui.ViewModels.Controls.ModuleExplorer;

namespace Reemit.Gui.Views.Controls.ModuleExplorer;

public partial class ModuleExplorerModuleNodeView : ReactiveUserControl<ModuleExplorerModuleNodeViewModel>
{
    public ModuleExplorerModuleNodeView()
    {
        InitializeComponent();
        this.WhenActivated(disposable =>
        {
            this.OneWayBind(ViewModel, vm => vm.Name.Value, v => v.NodeView.Text)
                .DisposeWith(disposable);
        });
    }
}