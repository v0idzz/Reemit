using System;
using System.Linq;
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
            this.OneWayBind(ViewModel, vm => vm.Modules, v => v.OpenAssembliesText.Text,
                    v => string.Join(Environment.NewLine, v.Select(x => x.DebugDump())))
                .DisposeWith(disposable);
        });
    }
}