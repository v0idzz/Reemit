using Avalonia.ReactiveUI;
using Reemit.Gui.ViewModels;

namespace Reemit.Gui.Views;

public partial class HelloView : ReactiveUserControl<HomeViewModel>
{
    public HelloView()
    {
        InitializeComponent();
    }
}