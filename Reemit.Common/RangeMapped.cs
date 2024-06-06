using System.Runtime.CompilerServices;

namespace Reemit.Common;

public readonly struct RangeMapped<TValue>(int position, int length, TValue value) : IRangeMapped
{
    public int Position { get; } = position;
    public int Length { get; } = length;
    public TValue Value { get; } = value;

    public static implicit operator TValue(RangeMapped<TValue> rangeMapped) => rangeMapped.Value;

    public RangeMapped<TResult> With<TResult>(TResult otherValue) => new(Position, Length, otherValue);

    public RangeMapped<TResult> Cast<TResult>()
    {
        var v = Value;

        return With<TResult>(Unsafe.As<TValue, TResult>(ref v));
    }

    public RangeMapped<TResult> Select<TResult>(Func<TValue, TResult> selector) => With(selector(Value));
}