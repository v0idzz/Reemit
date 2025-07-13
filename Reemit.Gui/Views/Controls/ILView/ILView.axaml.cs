using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reemit.Gui.ViewModels.Controls.ILView;

namespace Reemit.Gui.Views.Controls.ILView;

public partial class ILView : ReactiveUserControl<ILViewModel>
{
    public ILView()
    {
        InitializeComponent();

        this.WhenActivated(disposable =>
        {
            this.OneWayBind(ViewModel, vm => vm.ILCode, v => v.TextEditor.Text)
                .DisposeWith(disposable);
        });
    }
}