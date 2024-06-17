using Avalonia.ReactiveUI;
using AvaloniaHex.Document;
using ReactiveUI;
using Reemit.Gui.ViewModels.Controls.HexEditor;
using System;
using System.Reactive.Disposables;

namespace Reemit.Gui.Views.Controls.HexEditor;

public partial class HexEditorView : ReactiveUserControl<HexEditorViewModel>
{
    public HexEditorView()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            this
                .OneWayBind(ViewModel, x => x.ModuleDocument, x => x.ReemitHexEditor.Document)
                .DisposeWith(d);
        });
    }
}
