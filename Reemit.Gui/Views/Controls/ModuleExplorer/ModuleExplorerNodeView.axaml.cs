using Avalonia;
using Avalonia.Controls;
using Reemit.Gui.Models;
using Reemit.Gui.Views.Controls.Icons;

namespace Reemit.Gui.Views.Controls.ModuleExplorer;

public partial class ModuleExplorerNodeView : UserControl
{
    public static readonly DirectProperty<ModuleExplorerNodeView, IconKind> IconKindProperty =
        Icon.KindProperty.AddOwner<ModuleExplorerNodeView>(o => o.IconKind,
            (o, v) => o.IconKind = v);

    private IconKind _iconKind = IconKind.Class;
    
    public IconKind IconKind
    {
        get => _iconKind;
        set => SetAndRaise(IconKindProperty, ref _iconKind, value);
    }
    
    public static readonly StyledProperty<string?> TextProperty =
        AvaloniaProperty.Register<ModuleExplorerNodeView, string?>(nameof(Text));
    
    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly StyledProperty<bool> IsIconVisibleProperty =
        AvaloniaProperty.Register<ModuleExplorerNodeView, bool>(nameof(IsIconVisible), true);

    public bool IsIconVisible
    {
        get => GetValue(IsIconVisibleProperty);
        set => SetValue(IsIconVisibleProperty, value);
    }

    public ModuleExplorerNodeView()
    {
        InitializeComponent();
    }
}