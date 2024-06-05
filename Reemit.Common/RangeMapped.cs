namespace Reemit.Common;

public class RangeMapped<TValue>(int position, int length, TValue value) : IRangeMapped
{
    public int Position { get; } = position;
    public int Length { get; } = length;
    public TValue Value { get; } = value;

    public static implicit operator TValue(RangeMapped<TValue> rangeMapped) => rangeMapped.Value;
}
