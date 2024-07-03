using Avalonia.Controls;
using Avalonia.Metadata;
using Avalonia.ReactiveUI;
using AvaloniaHex.Editing;
using ReactiveUI;
using Reemit.Common;
using Reemit.Gui.ViewModels.Controls.HexEditor;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Reemit.Gui.Views.Controls.HexEditor;

public partial class HexEditorView : ReactiveUserControl<HexEditorViewModel>
{
    private int? _lastBytesPerLine;

    public HexEditorView()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            this.OneWayBind(ViewModel, x => x.ModuleDocument, x => x.ReemitHexEditor.Document)
                .DisposeWith(d);

            this.OneWayBind(ViewModel, x => x.ByteWidths, x => x.HexWidthCombobox.ItemsSource)
                .DisposeWith(d);

            this.Bind(ViewModel, x => x.CurrentByteWidth, x => x.HexWidthCombobox.SelectedItem)
                .DisposeWith(d);

            ViewModel
                .WhenAnyValue(x => x.CurrentByteWidth)
                .WhereNotNull()
                .Select(x => x.Width)
                .BindTo(ReemitHexEditor.HexView, x => x.BytesPerLine)
                .DisposeWith(d);

            _lastBytesPerLine = ReemitHexEditor.HexView.BytesPerLine;

            ReemitHexEditor.HexView
                .WhenAnyValue(x => x.BytesPerLine)
                .Subscribe(_ => BringSelectionIntoView())
                .DisposeWith(d);

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
                .Subscribe(_ => BringSelectionIntoView())
                .DisposeWith(d);

            ViewModel
                .WhenAnyValue(x => x.SelectedRange)
                .WhereNotNull()
                .BindTo(ReemitHexEditor, x => x.Selection.Range)
                .DisposeWith(d);

            this.BindCommand(ViewModel, x => x.Navigation.NextCommand, x => x.NavigateNextButton)
                .DisposeWith(d);

            this.BindCommand(ViewModel, x => x.Navigation.PreviousCommand, x => x.NavigatePreviousButton)
                .DisposeWith(d);

            this.OneWayBind(ViewModel, x => x.SelectionOffset, x => x.OffsetTextBox.Text);
            this.OneWayBind(ViewModel, x => x.SelectionEnd, x => x.EndTextBox.Text);
            this.OneWayBind(ViewModel, x => x.SelectionLength, x => x.LengthTextBox.Text);

            Observable
                .FromEvent<EventHandler, EventArgs>(
                    h => (o, e) => h(e),
                    h => ReemitHexEditor.HexView.LayoutUpdated += h,
                    h => ReemitHexEditor.HexView.LayoutUpdated -= h)
                .Where(_ => _lastBytesPerLine != ReemitHexEditor.HexView.BytesPerLine)
                .Subscribe(_ =>
                {
                    _lastBytesPerLine = ReemitHexEditor.HexView.BytesPerLine;
                    BringSelectionIntoView();
                })
                .DisposeWith(d);
        });
    }

    private void BringSelectionIntoView()
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
    }
}
