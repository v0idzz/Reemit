using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reemit.Gui.ViewModels.Controls.HexEditor;
using System;
using System.Linq;
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

            this.OneWayBind(ViewModel, x => x.ByteWidths, x => x.HexWidthCombobox.ItemsSource)
                .DisposeWith(d);

            this.Bind(
                ViewModel,
                x => x.CurrentByteWidth,
                x => x.HexWidthCombobox.SelectedItem,
                x =>
                {
                    Console.WriteLine("Binding VM to view");

                    return x;
                },
                x =>
                {
                    Console.WriteLine("Binding view to VM");

                    return (ByteWidthViewModel)x!;
                })
                .DisposeWith(d);

            ViewModel
                .WhenAnyValue(x => x.CurrentByteWidth)
                .WhereNotNull()
                .Select(x => x.Width)
                .BindTo(ReemitHexEditor.HexView, x => x.BytesPerLine);

            var selection = ReemitHexEditor.Selection;

            var rangeChanged = Observable
                .FromEvent<EventHandler, EventArgs>(
                    h => (o, e) => h(e),
                    h => selection.RangeChanged += h,
                    h => selection.RangeChanged -= h);

            rangeChanged
                .Select(_ => selection.Range)
                .BindTo(ViewModel, x => x.SelectedRange)
                .DisposeWith(d);

            rangeChanged
                .Subscribe(_ =>
                {
                    var hv = ReemitHexEditor.HexView;
                    var initialYOffset = hv.ScrollOffset.Y;
                    var r = ReemitHexEditor.Selection.Range;
                    hv.BringIntoView(r.Start);

                    if (r.End.ByteIndex - r.Start.ByteIndex > 1 &&
                        hv.ScrollOffset.Y >= initialYOffset)
                    {
                        hv.BringIntoView(r.End);
                    }
                })
                .DisposeWith(d);

            ViewModel
                .WhenAnyValue(x => x.SelectedRange)
                .WhereNotNull()
                .Do(x =>
                {
                    Console.WriteLine("SelectedRange changed");
                })
                .BindTo(ReemitHexEditor, x => x.Selection.Range)
                .DisposeWith(d);

            this.BindCommand(ViewModel, x => x.Navigation.NavigateNextCommand, x => x.NavigateNextButton);
            this.BindCommand(ViewModel, x => x.Navigation.NavigatePreviousCommand, x => x.NavigatePreviousButton);
        });
    }
}
