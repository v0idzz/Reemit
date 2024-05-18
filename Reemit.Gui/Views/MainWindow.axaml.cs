using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reemit.Gui.ViewModels;

namespace Reemit.Gui.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();

        this.WhenActivated(disposable =>
        {
            this.OneWayBind(ViewModel, vm => vm.Router, v => v.RoutedViewHost.Router)
                .DisposeWith(disposable);
        });
    }
}