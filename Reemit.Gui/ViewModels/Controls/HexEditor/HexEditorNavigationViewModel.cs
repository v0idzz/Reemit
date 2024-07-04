using AvaloniaHex.Document;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reemit.Gui.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace Reemit.Gui.ViewModels.Controls.HexEditor;

public class HexEditorNavigationViewModel : ReactiveObject
{
    private readonly HexEditorViewModel _hexEditorViewModel;

    [Reactive]
    public BitRange? NavigationBitRange { get; set; }

    [ObservableAsProperty]
    public HexNavigationRangeViewModel? ResolvedNavigationRange { get; }

    [Reactive]
    public ObservableCollection<HexNavigationRangeViewModel> NavigationRanges { get; set; } = [];

    public ReactiveCommand<Unit, Unit> NextCommand { get; }

    public ReactiveCommand<Unit, Unit> PreviousCommand { get; }

    public HexEditorNavigationViewModel(HexEditorViewModel hexEditorViewModel)
    {
        _hexEditorViewModel = hexEditorViewModel;

        this.WhenAnyValue(x => x.NavigationBitRange, x => x.NavigationRanges)
            .Where(x => x.Item1 != null)
            .Select(x =>
            {
                var (range, knownRanges) = x;

                return knownRanges
                    .Where(y =>
                        y.RangeMapped.Position <= (int)range!.Value.Start.ByteIndex &&
                        (int)range!.Value.End.ByteIndex <= y.RangeMapped.End)
                    .OrderByDescending(x => x.RangeMapped.Position)
                    .ThenBy(x => x.RangeMapped.Length)
                    .FirstOrDefault();
            })
            .ToPropertyEx(this, x => x.ResolvedNavigationRange);

        this.WhenAnyValue(x => x.ResolvedNavigationRange)
            .Buffer(2, 1)
            .Subscribe(x =>
            {
                x.First()?.Leave();
                x.Last()?.Navigate();
            });

        NavigationMessageBus
            .ListenForRegistration()
            .Select(x => new HexNavigationRangeViewModel(x.RangeMapped, x.Navigate, x.Leave))
            .Subscribe(NavigationRanges.Add);

        NextCommand = ReactiveCommand.Create(NavigateNext);
        PreviousCommand = ReactiveCommand.Create(NavigatePrevious);
    }

    private void Navigate(
        Func<
            IEnumerable<HexNavigationRangeViewModel>,
            Func<HexNavigationRangeViewModel, int>,
            IOrderedEnumerable<HexNavigationRangeViewModel>>
                orderFunc,
        Func<HexNavigationRangeViewModel, bool> predicate)
    {
        HexNavigationRangeViewModel nextRange;

        if (!NavigationRanges.Any())
        {
            return;
        }
        else
        {
            var ordered = orderFunc(NavigationRanges, x => x.RangeMapped.Position);

            if (_hexEditorViewModel.SelectedRange == null)
            {
                nextRange = ordered.First();
            }
            else
            {
                var eagerOrdered = ordered.ToArray();

                nextRange = eagerOrdered.FirstOrDefault(predicate) ?? eagerOrdered.First();
            }
        }

        _hexEditorViewModel.SelectedRange = new BitRange(
            (ulong)nextRange.RangeMapped.Position,
            (ulong)nextRange.RangeMapped.End);
    }

    private void NavigateNext() =>
        Navigate(
            Enumerable.OrderBy,
            x => x.RangeMapped.Position > (int)_hexEditorViewModel.SelectedRange!.Value.Start.ByteIndex);

    private void NavigatePrevious() =>
        Navigate(
            Enumerable.OrderByDescending,
            x => x.RangeMapped.Position < (int)_hexEditorViewModel.SelectedRange!.Value.Start.ByteIndex);
}
