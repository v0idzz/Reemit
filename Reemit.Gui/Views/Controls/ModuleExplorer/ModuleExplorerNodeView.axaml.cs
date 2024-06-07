using Avalonia;
using Avalonia.Controls;

namespace Reemit.Gui.Views.Controls.ModuleExplorer;

public partial class ModuleExplorerNodeView : UserControl
{
    public static readonly StyledProperty<object?> IconProperty =
        AvaloniaProperty.Register<ModuleExplorerNodeView, object?>(nameof(Icon));
    
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
    
    public static readonly StyledProperty<string?> TextProperty =
        AvaloniaProperty.Register<ModuleExplorerNodeView, string?>(nameof(Text));
    
    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ModuleExplorerNodeView()
    {
        InitializeComponent();
    }
}