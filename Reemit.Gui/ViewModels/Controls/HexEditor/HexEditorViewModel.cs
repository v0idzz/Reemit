using Avalonia;
using AvaloniaHex.Document;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reemit.Common;
using Reemit.Gui.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace Reemit.Gui.ViewModels.Controls.HexEditor;

public class HexEditorViewModel : ReactiveObject
{
    public IReadOnlyCollection<ByteWidthViewModel> ByteWidths { get; } =
    [
        new ByteWidthViewModel(null),
        new ByteWidthViewModel(8),
        new ByteWidthViewModel(16),
        new ByteWidthViewModel(32),
        new ByteWidthViewModel(64),
        new ByteWidthViewModel(128),
    ];

    [Reactive]
    public ByteWidthViewModel CurrentByteWidth { get; set; }

    public HexEditorNavigationViewModel Navigation { get; } = new();

    [Reactive]
    public IReadOnlyCollection<byte>? ModuleBytes { get; set; }

    [ObservableAsProperty]
    public ByteArrayBinaryDocument? ModuleDocument { get; }

    public ReactiveCommand<Unit, Unit> NavigateNextCommand { get; }

    public ReactiveCommand<Unit, Unit> NavigatePreviousCommand { get; }

    [Reactive]
    public BitRange? SelectedRange { get; set; }

    public HexEditorViewModel()
    {
        this.CurrentByteWidth = ByteWidths.First();

        this.WhenAnyValue(x => x.ModuleBytes)
            .Select(x => x != null ? new ByteArrayBinaryDocument(x.ToArray(), true) : null)
            .ToPropertyEx(this, x => x.ModuleDocument);

        NavigationMessageBus
            .ListenForNavigation()
            .Select(x => x.Range)
            .BindTo(this, x => x.SelectedRange);

        this.WhenAnyValue(x => x.SelectedRange)
            .BindTo(Navigation, x => x.NavigationBitRange);

        NavigateNextCommand = ReactiveCommand.Create(NavigateNext);
        NavigatePreviousCommand = ReactiveCommand.Create(NavigatePrevious);
    }

    // Todo: refactor into generic Next/Prev method, find a way to make it
    // work in nav vm.
    private void NavigateNext()
    {
        HexNavigationRangeViewModel nextRange;

        if (!Navigation.NavigationRanges.Any())
        {
            return;
        }
        else
        {
            var ordered = Navigation.NavigationRanges.OrderBy(x => x.RangeMapped.Position);

            if (SelectedRange == null)
            {
                nextRange = ordered.First();
            }
            else
            {
                var nextOrDefault = ordered
                    .FirstOrDefault(x => x.RangeMapped.Position > (int)SelectedRange.Value.Start.ByteIndex);

                nextRange = nextOrDefault ?? ordered.First();
            }
        }

        SelectedRange = new BitRange(
            (ulong)nextRange.RangeMapped.Position,
            (ulong)(nextRange.RangeMapped.End));
    }

    private void NavigatePrevious()
    {
        HexNavigationRangeViewModel nextRange;

        if (!Navigation.NavigationRanges.Any())
        {
            return;
        }
        else
        {
            var ordered = Navigation.NavigationRanges.OrderByDescending(x => x.RangeMapped.Position);

            if (SelectedRange == null)
            {
                nextRange = ordered.First();
            }
            else
            {
                var nextOrDefault = ordered
                    .FirstOrDefault(x => x.RangeMapped.Position < (int)SelectedRange.Value.Start.ByteIndex);

                nextRange = nextOrDefault ?? ordered.First();
            }
        }

        SelectedRange = new BitRange(
            (ulong)nextRange.RangeMapped.Position,
            (ulong)(nextRange.RangeMapped.End));
    }
}
