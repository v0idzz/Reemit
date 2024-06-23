using ReactiveUI;
using Reemit.Common;
using System;

namespace Reemit.Gui.ViewModels.Controls.HexEditor;

public class HexNavigationRangeViewModel(IRangeMapped rangeMapped, Action navigate) : ReactiveObject
{
    public IRangeMapped RangeMapped { get; } = rangeMapped;

    public Action Navigate { get; } = navigate;
}
