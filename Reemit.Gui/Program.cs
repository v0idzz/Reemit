using Avalonia;
using Avalonia.ReactiveUI;
using Reemit.Common;
using Reemit.Gui.ViewModels.Controls.HexEditor;
using System;
using System.Diagnostics;

namespace Reemit.Gui;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        var vm = new HexEditorNavigationViewModel();
        Debug.Assert(vm.NavigationRangeTable.Count == 0);

        vm.NavigationRanges =
        [
            new HexNavigationRangeViewModel(
                new RangeMapped<string>(1, 2, "foo"),
                () => { })
        ];

        vm.NavigationBitRange = new AvaloniaHex.Document.BitRange(0, 2);

        //vm.NavigationRanges.Add(
        //    new HexNavigationRangeViewModel(
        //        new RangeMapped<string>(0, 2, "foo"),
        //        () => { }));

        Debug.Assert(vm.NavigationRangeTable.Count == 1);

        BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}