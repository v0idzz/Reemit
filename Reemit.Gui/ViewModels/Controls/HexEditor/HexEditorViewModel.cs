using AvaloniaHex.Document;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;

namespace Reemit.Gui.ViewModels.Controls.HexEditor;

public class HexEditorViewModel : ReactiveObject
{
    public HexEditorNavigationViewModel Navigation { get; } = new();

    [Reactive]
    public IReadOnlyCollection<byte>? ModuleBytes { get; set; }

    [ObservableAsProperty]
    public ByteArrayBinaryDocument? ModuleDocument { get; }

    [Reactive]
    public BitRange? SelectedRange { get; set; }

    public HexEditorViewModel()
    {
        this.WhenAnyValue(x => x.ModuleBytes)
            .Select(x => x != null ? new ByteArrayBinaryDocument(x.ToArray(), true) : null)
            .Do(x =>
            {
                Debug.WriteLine("HexEditorViewModel.ModuleDocument changed.");
            })
            .ToPropertyEx(this, x => x.ModuleDocument);

        this.WhenAnyValue(x => x.SelectedRange)
            .Do(x =>
            {
                if (x != null)
                {
                    Debug.WriteLine($"HexEditorViewModel.SelectedRange changed: {x.Value.ToString()}");
                }
            })
            .BindTo(Navigation, x => x.NavigationBitRange);
    }
}
