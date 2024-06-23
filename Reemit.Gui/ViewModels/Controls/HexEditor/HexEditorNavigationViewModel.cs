﻿using AvaloniaHex.Document;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Reemit.Gui.ViewModels.Controls.HexEditor;

public class HexEditorNavigationViewModel : ReactiveObject
{
    [Reactive]
    public BitRange? NavigationBitRange { get; set; }

    [ObservableAsProperty]
    public HexNavigationRangeViewModel? ResolvedNavigationRange { get; set; }

    [Reactive]
    public IReadOnlyCollection<HexNavigationRangeViewModel> NavigationRanges { get; set; } = [];

    public HexEditorNavigationViewModel()
    {
        this.WhenAnyValue(x => x.NavigationBitRange, x => x.NavigationRanges)
            .Where(x => x.Item1 != null)
            .Select(x =>
            {
                var (range, knownRanges) = x;

                return knownRanges
                    .Where(x =>
                        x.RangeMapped.Position <= (int)range!.Value.Start.ByteIndex &&
                        (int)range!.Value.End.ByteIndex <= x.RangeMapped.End)
                    .OrderByDescending(x => x.RangeMapped.Position)
                    .ThenBy(x => x.RangeMapped.Length)
                    .FirstOrDefault();
            })
            .WhereNotNull()
            .ToPropertyEx(this, x => x.ResolvedNavigationRange);
    }
}