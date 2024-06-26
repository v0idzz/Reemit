using AvaloniaHex.Document;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reemit.Gui.ViewModels.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Reemit.Gui.ViewModels.Controls.HexEditor;

public class HexEditorViewModel : ReactiveObject
{
    public IReadOnlyCollection<ByteWidthViewModel> ByteWidths { get; } =
    [
        new ByteWidthViewModel(null),
        new ByteWidthViewModel(4),
        new ByteWidthViewModel(8),
        new ByteWidthViewModel(16),
        new ByteWidthViewModel(32),
        new ByteWidthViewModel(64),
        new ByteWidthViewModel(128),
    ];

    [Reactive]
    public ByteWidthViewModel CurrentByteWidth { get; set; }

    public HexEditorNavigationViewModel Navigation { get; }

    [Reactive]
    public IReadOnlyCollection<byte>? ModuleBytes { get; set; }

    [ObservableAsProperty]
    public ByteArrayBinaryDocument? ModuleDocument { get; }

    [Reactive]
    public BitRange? SelectedRange { get; set; }

    [ObservableAsProperty]
    public int SelectionOffset { get; }

    [ObservableAsProperty]
    public int SelectionEnd { get; }

    [ObservableAsProperty]
    public int SelectionLength { get; }

    public HexEditorViewModel()
    {
        Navigation = new(this);
        CurrentByteWidth = ByteWidths.First();

        this.WhenAnyValue(x => x.ModuleBytes)
            .Select(x => x != null ? new ByteArrayBinaryDocument(x.ToArray(), true) : null)
            .ToPropertyEx(this, x => x.ModuleDocument);

        NavigationMessageBus
            .ListenForNavigation()
            .Select(x => x.Range)
            .BindTo(this, x => x.SelectedRange);

        var observableSelectedRange = this.WhenAnyValue(x => x.SelectedRange);

        observableSelectedRange.BindTo(Navigation, x => x.NavigationBitRange);

        var observableSelectedBitRange = observableSelectedRange.WhereNotNull().Select(x => x!.Value);

        observableSelectedBitRange
            .Select(x => (int)x.Start.ByteIndex)
            .ToPropertyEx(this, x => x.SelectionOffset);

        observableSelectedBitRange
            .Select(x => (int)x.End.ByteIndex)
            .ToPropertyEx(this, x => x.SelectionEnd);

        observableSelectedBitRange
            .Select(x => (int)x.ByteLength)
            .ToPropertyEx(this, x => x.SelectionLength);
    }
}
