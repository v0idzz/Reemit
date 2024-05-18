using System;
using System.Reactive.Disposables;
using ReactiveUI;

namespace Reemit.Gui.ViewModels;

public class MainWindowViewModel : ViewModelBase, IScreen, IActivatableViewModel
{
    public RoutingState Router { get; } = new();

    public MainWindowViewModel()
    {
        Activator = new ViewModelActivator();
        this.WhenActivated(disposable =>
        {
            Router.Navigate.Execute(new HomeViewModel(this))
                .Subscribe()
                .DisposeWith(disposable);
        });
    }

    public ViewModelActivator Activator { get; }
}