using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reemit.Gui.ViewModels;

namespace Reemit.Gui.Views;

public partial class HomeView : ReactiveUserControl<HomeViewModel>
{
    public HomeView()
    {
        InitializeComponent();

        this.WhenActivated(disposable =>
        {
            this.OneWayBind(ViewModel, vm => vm.Layout, v => v.RootDock.Layout)
                .DisposeWith(disposable);
        });
    }
}