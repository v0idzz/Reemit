using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Reemit.Gui.Common;
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

    private double _themeLuminosity;

    public IconKind Kind
    {
        get => _kind;
        set => SetAndRaise(KindProperty, ref _kind, value);
    }
    
    public Icon()
    {
        InitializeComponent();

        Update();
        ActualThemeVariantChanged += (_, _) => Update();
    }

    private void Update()
    {
        UpdateThemeLuminosity();
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
    
    private void UpdateThemeLuminosity()
    {
        const string bgColorResourceKey = "SystemRegionColor";
        Application.Current!.TryGetResource(bgColorResourceKey, Application.Current.ActualThemeVariant,
            out var resource);

        if (resource is not Color bgColor)
        {
            throw new ApplicationException("Couldn't get theme background color required to theme the icon");
        }

        _themeLuminosity = bgColor.ToHsl().L;
    }

    private void UpdateIcon()
    {
        var icon = (Control)(Kind switch
        {
            IconKind.Class => new ClassIcon(),
            IconKind.Interface => new InterfaceIcon(),
            IconKind.Module => new ModuleIcon(),
            IconKind.Namespace => new NamespaceIcon(),
            IconKind.Structure => new StructureIcon(),
            IconKind.Method => new MethodIcon(),
            _ => throw new ArgumentOutOfRangeException(nameof(Kind), Kind, "Unrecognized icon kind")
        });

        AdjustIconLuminosity(icon);

        Children.Clear();
        Children.Add(icon);
    }

    private void AdjustIconLuminosity(Control icon)
    {
        // This looks hack-ish, but works for all the VS icons we currently have.
        if (icon.GetLogicalChildren().FirstOrDefault()?.GetLogicalChildren().FirstOrDefault() is Rectangle iconRectangle)
        {
            foreach (var brush in iconRectangle.Resources.Values.OfType<SolidColorBrush>())
            {
                brush.Color = brush.Color.TransformLuminosity(_themeLuminosity);
            }
        }
    }
}