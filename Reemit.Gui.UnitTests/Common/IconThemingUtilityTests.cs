using Avalonia.Media;
using Reemit.Gui.Common;

namespace Reemit.Gui.UnitTests.Common;

public class IconThemingUtilityTests
{
    [Theory]
    [InlineData(0xFF996F00u, 0.18235294117647058, 0xFFFFDB7Du)]
    [InlineData(0xFF6936AAu, 0, 0xFF844FC7u)]
    public void TransformLuminosity_CalledWithColor_TransformsColorLuminosityToMatchBackgroundLuminosity(
        uint sourceColor,
        double backgroundLuminosity,
        uint expectedColor)
    {
        // Arrange
        var source = Color.FromUInt32(sourceColor);
        
        // Act
        var result = source.TransformLuminosity(backgroundLuminosity);
        
        // Assert
        Assert.Equal(Color.FromUInt32(expectedColor), result);
    }
}