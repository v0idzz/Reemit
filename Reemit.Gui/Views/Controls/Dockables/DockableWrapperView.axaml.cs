using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reemit.Gui.ViewModels.Dockables;

namespace Reemit.Gui.Views.Controls.Dockables;

public partial class DockableWrapperView : ReactiveUserControl<IDockableViewModel>
{
    public DockableWrapperView()
    {
        InitializeComponent();

        this.WhenActivated(disposable =>
        {
            this.OneWayBind(ViewModel, vm => vm.ViewModel, v => v.ContentControl.Content)
                .DisposeWith(disposable);
        });
    }
}