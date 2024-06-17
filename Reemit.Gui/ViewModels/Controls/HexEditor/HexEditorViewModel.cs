using AvaloniaHex.Document;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive.Linq;

namespace Reemit.Gui.ViewModels.Controls.HexEditor;

public class HexEditorViewModel : ReactiveObject
{
    [Reactive]
    public IReadOnlyCollection<byte>? ModuleBytes { get; set; }

    [ObservableAsProperty]
    public ByteArrayBinaryDocument? ModuleDocument { get; }

    public HexEditorViewModel()
    {
        this.WhenAnyValue(x => x.ModuleBytes)
            .Select(x => x != null ? new ByteArrayBinaryDocument(x.ToArray(), true) : null)
            .Do(x =>
            {
                Console.WriteLine();
            })
            .ToPropertyEx(this, x => x.ModuleDocument);

        //ModuleBytes = new byte[] { 0xde, 0xad }.ToImmutableArray();
    }
}
