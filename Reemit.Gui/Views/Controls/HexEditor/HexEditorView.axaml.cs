using Avalonia.ReactiveUI;
using AvaloniaHex.Document;
using ReactiveUI;
using Reemit.Gui.ViewModels.Controls.HexEditor;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Reemit.Gui.Views.Controls.HexEditor;

public partial class HexEditorView : ReactiveUserControl<HexEditorViewModel>
{
    public HexEditorView()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            this.OneWayBind(ViewModel, x => x.ModuleDocument, x => x.ReemitHexEditor.Document)
                .DisposeWith(d);

            var selection = ReemitHexEditor.Selection;

            Observable
                .FromEvent<EventHandler, EventArgs>(
                    h => (o, e) => h(e),
                    h => selection.RangeChanged += h,
                    h => selection.RangeChanged -= h)
                .Select(_ => selection.Range)
                .BindTo(ViewModel, x => x.SelectedRange)
                .DisposeWith(d);

            ViewModel
                .WhenAnyValue(x => x.SelectedRange)
                .WhereNotNull()
                .BindTo(ReemitHexEditor, x => x.Selection.Range)
                .DisposeWith(d);
        });
    }
}
