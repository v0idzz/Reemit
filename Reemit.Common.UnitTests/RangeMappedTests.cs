namespace Reemit.Common.UnitTests;

public sealed class RangeMappedTests
{
    [Fact]
    public void With_Called_ConstructsWithNewValue()
    {
        // Arrange
        var rangeMapped = new RangeMapped<int>(0x52, 0x30, 0x1592);

        // Act
        var actualRangeMapped = rangeMapped.With(0xdeadbeef);

        // Assert
        AssertLengthAndPosition(rangeMapped, actualRangeMapped);
        Assert.Equal(0xdeadbeef, actualRangeMapped.Value);
        Assert.IsType<uint>(actualRangeMapped.Value);
        Assert.IsType<RangeMapped<uint>>(actualRangeMapped);
    }

    [Fact]
    public void Select_Called_ConstructsWithNewValue()
    {
        // Arrange
        var rangeMapped = new RangeMapped<int>(0x52, 0x30, 0x1000);

        // Act
        var actualRangeMapped = rangeMapped.Select(x => x * 2);

        // Assert
        AssertLengthAndPosition(rangeMapped, actualRangeMapped);
        Assert.Equal(0x2000, actualRangeMapped.Value);        
    }

    [Fact]
    public void Cast_Called_ConstructsWithNewValue()
    {
        // Arrange
        var rangeMapped = new RangeMapped<int>(0x52, 0x30, 0x1592);

        // Act
        var actualRangeMapped = rangeMapped.Cast<uint>();

        // Assert
        AssertLengthAndPosition(rangeMapped, actualRangeMapped);
        Assert.Equal((uint)0x1592, actualRangeMapped.Value);
        Assert.IsType<uint>(actualRangeMapped.Value);
        Assert.IsType<RangeMapped<uint>>(actualRangeMapped);
    }

    private void AssertLengthAndPosition(IRangeMapped expected, IRangeMapped actual)
    {
        Assert.Equal(expected.Length, actual.Length);
        Assert.Equal(expected.Position, actual.Position);
    }
}
