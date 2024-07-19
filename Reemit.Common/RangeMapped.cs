using System.Runtime.CompilerServices;

namespace Reemit.Common;

public readonly record struct RangeMapped<TValue>(int Position, int Length, TValue Value) : IRangeMapped
{
    public int End => Position + Length;

    public static implicit operator TValue(RangeMapped<TValue> rangeMapped) => rangeMapped.Value;

    public RangeMapped<TResult> With<TResult>(TResult otherValue) => new(Position, Length, otherValue);

    public RangeMapped<TValue> At(int offset) => this with { Position = Position + offset };

    public RangeMapped<TResult> Cast<TResult>()
    {
        var v = Value;

        return With(Unsafe.As<TValue, TResult>(ref v));
    }

    public RangeMapped<TResult> Select<TResult>(Func<TValue, TResult> selector) => With(selector(Value));
}