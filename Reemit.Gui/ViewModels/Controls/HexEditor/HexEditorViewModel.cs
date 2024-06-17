using AvaloniaHex.Document;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace Reemit.Gui.ViewModels.Controls.HexEditor;

public class HexEditorViewModel : ReactiveObject
{
    [Reactive]
    public ReadOnlyCollection<byte>? ModuleBytes { get; set; }

    [ObservableAsProperty]
    public ByteArrayBinaryDocument? ModuleDocument { get; }

    public HexEditorViewModel()
    {
        this
            .WhenAnyValue(x => x.ModuleBytes)
            .Select(x => x != null ? new ByteArrayBinaryDocument(x.ToArray(), true) : null)
            .ToPropertyEx(this, x => x.ModuleDocument);
    }
}
