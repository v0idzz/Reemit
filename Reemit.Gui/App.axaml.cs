using System;
using System.Reflection;
using System.Xml;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.Highlighting.Xshd;
using ReactiveUI;
using Reemit.Gui.ViewModels;
using Reemit.Gui.Views;
using Splat;

namespace Reemit.Gui;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    public override void RegisterServices()
    {
        RegisterILHighlightingTheme("ILLight");
        RegisterILHighlightingTheme("ILDark");

        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());

        base.RegisterServices();
    }

    private void RegisterILHighlightingTheme(string themeName)
    {
        using var stream = AssetLoader.Open(new Uri($"avares://Reemit.Gui/Assets/{themeName}.xshd"));
        using var reader = XmlReader.Create(stream);

        var highlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
        HighlightingManager.Instance.RegisterHighlighting(themeName, [".il"], highlighting);
    }
}