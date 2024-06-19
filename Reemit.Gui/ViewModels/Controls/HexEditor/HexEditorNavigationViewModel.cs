using Avalonia.Collections;
using AvaloniaHex.Document;
using AvaloniaHex.Editing;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reemit.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;

namespace Reemit.Gui.ViewModels.Controls.HexEditor;

public class HexNavigationRangeViewModel(IRangeMapped rangeMapped, Action navigate) : ReactiveObject
{
    public IRangeMapped RangeMapped { get; } = rangeMapped;

    public Action Navigate { get; } = navigate;
}

public class HexEditorNavigationViewModel : ReactiveObject
{
    [Reactive]
    public BitRange? NavigationBitRange { get; set; }

    [ObservableAsProperty]
    public HexNavigationRangeViewModel? ResolvedNavigationRange { get; set; }

    [Reactive]
    public IReadOnlyCollection<HexNavigationRangeViewModel> NavigationRanges { get; set; }
     
    [ObservableAsProperty]
    public ReadOnlyDictionary<int, HexNavigationRangeViewModel> NavigationRangeTable { get; }

    public HexEditorNavigationViewModel()
    {
        this.WhenAnyValue(x => x.NavigationRanges)
            .WhereNotNull()
            .Select(x => x
                .GroupBy(y => y.RangeMapped.Position)
                .ToDictionary(
                    y => y.Key,
                    y => y
                        .OrderBy(x => x.RangeMapped.Length)
                        .First()))
            .Select(x => new ReadOnlyDictionary<int, HexNavigationRangeViewModel>(x))
            .Do(x =>
            {
                Debug.WriteLine("test");
            })
            .ToPropertyEx(this, x => x.NavigationRangeTable)
            ;

        NavigationRanges = [];
    }
}
