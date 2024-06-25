using ReactiveUI;
using Reemit.Common;
using System;

namespace Reemit.Gui.ViewModels.Controls.HexEditor;

public class HexNavigationRangeViewModel(
    IRangeMapped rangeMapped,
    Action navigate,
    Action leave) : ReactiveObject
{
    public IRangeMapped RangeMapped { get; } = rangeMapped;

    public Action Navigate { get; } = navigate;

    public Action Leave { get; } = leave;
}
