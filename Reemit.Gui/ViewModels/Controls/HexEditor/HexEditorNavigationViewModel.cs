using Avalonia.Collections;
using AvaloniaHex.Document;
using AvaloniaHex.Editing;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reemit.Common;
using System;
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

    public ObservableCollection<HexNavigationRangeViewModel> NavigationRanges { get; } = new();

    [ObservableAsProperty]
    public ReadOnlyDictionary<int, HexNavigationRangeViewModel> NavigationRangeTable { get; }

    public HexEditorNavigationViewModel()
    {
        this.NavigationRanges
            .ToObservable()
            .GroupBy(x => x.RangeMapped.Position)
            .ToDictionary(
                x => x.Key,
                x => x
                    .ToEnumerable()
                    .OrderBy(x => x.RangeMapped.Length)
                    .First())
            .Select(x => new ReadOnlyDictionary<int, HexNavigationRangeViewModel>(x))
            .Do(x =>
            {
                Debug.WriteLine("test");
            })
            .ToPropertyEx(this, x => x.NavigationRangeTable);
    }
}
