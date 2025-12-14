using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.ReactiveUI;
using AvaloniaEdit.Highlighting;
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

            Observable.FromEventPattern(handler => Application.Current!.ActualThemeVariantChanged += handler,
                    handler => Application.Current!.ActualThemeVariantChanged -= handler)
                .Select(_ => (string)Application.Current!.ActualThemeVariant.Key == "Dark")
                .StartWith((string)Application.Current!.ActualThemeVariant.Key == "Dark")
                .Select(isDark => HighlightingManager.Instance.GetDefinition(isDark ? "ILDark" : "ILLight"))
                .BindTo(TextEditor, v => v.SyntaxHighlighting)
                .DisposeWith(disposable);
        });
    }
}