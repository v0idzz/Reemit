namespace Reemit.Gui.ViewModels.Controls.HexEditor;

public class ByteWidthViewModel(int? width)
{
    public int? Width { get; } = width;

    public string WidthName => Width?.ToString() ?? "Auto";
}
