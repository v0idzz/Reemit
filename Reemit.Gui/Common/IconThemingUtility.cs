using Avalonia.Media;

namespace Reemit.Gui.Common;

public static class IconThemingUtility
{
    // Based on https://stackoverflow.com/a/37501135
    public static double TransformLuminosity(double backgroundLuminosity, double luminosity)
    {
        var haloLuminosity = Color.FromArgb(255, 246, 246, 246).ToHsl().L;

        if (backgroundLuminosity < 0.5)
        {
            haloLuminosity = 1.0 - haloLuminosity;
            luminosity = 1.0 - luminosity;
        }

        if (luminosity < haloLuminosity)
        {
            return backgroundLuminosity * luminosity / haloLuminosity;
        }

        return (1.0 - backgroundLuminosity) * (luminosity - 1.0) / (1.0 - haloLuminosity) + 1.0;
    }

    public static Color TransformLuminosity(this Color color, double backgroundLuminosity)
    {
        var hslSourceColor = color.ToHsl();
        var l = TransformLuminosity(backgroundLuminosity, hslSourceColor.L);
        var result = new HslColor(hslSourceColor.A, hslSourceColor.H, hslSourceColor.S, l);
        return result.ToRgb();
    }
}