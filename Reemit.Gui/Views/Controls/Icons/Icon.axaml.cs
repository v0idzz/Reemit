using Avalonia;
using Avalonia.Controls;
using Reemit.Gui.Models;

namespace Reemit.Gui.Views.Controls.Icons;

public partial class Icon : Panel
{
    public static readonly DirectProperty<Icon, IconKind> KindProperty =
        AvaloniaProperty.RegisterDirect<Icon, IconKind>(
            nameof(Kind),
            o => o.Kind,
            (o, v) => o.Kind = v);

    private IconKind _kind = IconKind.Class;

    public IconKind Kind
    {
        get => _kind;
        set => SetAndRaise(KindProperty, ref _kind, value);
    }
    
    public Icon()
    {
        InitializeComponent();
        UpdateIcon();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        if (change.Property == KindProperty)
        {
            UpdateIcon();
        }

        base.OnPropertyChanged(change);
    }

    private void UpdateIcon()
    {
        var icon = (Control)(Kind switch
        {
            IconKind.Class => new ClassIcon(),
            IconKind.Interface => new InterfaceIcon(),
            IconKind.Module => new ModuleIcon(),
            IconKind.Namespace => new NamespaceIcon(),
            IconKind.Structure => new StructureIcon()
        });
            
        Children.Clear();
        Children.Add(icon);
    }
}