namespace Reemit.Common;

// TODO: Consider allowing SharedReader to be used lock-free
public class SharedReader(int startOffset, BinaryReader reader, object lockObj) : BinaryReader(reader.BaseStream)
{
    public class SharedReaderRangeScope(List<SharedReaderRangeScope> owner, int position) : IDisposable
    {
        public int Position { get; } = position;

        public int Length { get; internal set; }

        public void Dispose() => owner.Remove(this);

        public RangeMapped<TValue> ToRangeMapped<TValue>(TValue value) => new(Position, Length, value);
    }

    private readonly int _startOffset = startOffset;

    private readonly ThreadLocal<List<SharedReaderRangeScope>> _rangeScopes = new(() => new());

    public int Offset { get; private set; } = startOffset;

    public object SynchronizationObject => lockObj;

    public int RelativeOffset => Offset - _startOffset;

    // Looking to easily avoid duplication here. If this
    // turns into a perf issues, we can address then.
    private T Read<T>(Func<T> readFunc) => ReadMapped(readFunc);

    private RangeMapped<T> ReadMapped<T>(Func<T> readFunc)
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            var startOffset = Offset;
            BaseStream.Seek(Offset, SeekOrigin.Begin);
            var value = readFunc();
            var size = (int)(BaseStream.Position - Offset);
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            Offset += size;

            foreach (var scope in _rangeScopes.Value!)
            {
                scope.Length += size;
            }

            return new RangeMapped<T>(startOffset, size, value);
        }
    }

    public SharedReaderRangeScope CreateRangeScope()
    {
        var owner = _rangeScopes.Value!;
        var scope = new SharedReaderRangeScope(owner, Offset);
        owner.Add(scope);

        return scope;
    }

    public override long ReadInt64() => Read(base.ReadInt64);

    public override int ReadInt32() => Read(base.ReadInt32);

    public override short ReadInt16() => Read(base.ReadInt16);

    public override ulong ReadUInt64() => Read(base.ReadUInt64);

    public override uint ReadUInt32() => Read(base.ReadUInt32);

    public override ushort ReadUInt16() => Read(base.ReadUInt16);

    public override byte[] ReadBytes(int count) => Read(() => base.ReadBytes(count));

    public override char ReadChar() => Read(base.ReadChar);

    public override byte ReadByte() => Read(base.ReadByte);

    public RangeMapped<long> ReadMappedInt64() => ReadMapped(base.ReadInt64);

    public RangeMapped<int> ReadMappedInt32() => ReadMapped(base.ReadInt32);

    public RangeMapped<short> ReadMappedInt16() => ReadMapped(base.ReadInt16);

    public RangeMapped<ulong> ReadMappedUInt64() => ReadMapped(base.ReadUInt64);

    public RangeMapped<uint> ReadMappedUInt32() => ReadMapped(base.ReadUInt32);

    public RangeMapped<ushort> ReadMappedUInt16() => ReadMapped(base.ReadUInt16);

    public RangeMapped<byte[]> ReadMappedBytes(int count) => ReadMapped(() => base.ReadBytes(count));

    public RangeMapped<char> ReadMappedChar() => ReadMapped(base.ReadChar);

    public RangeMapped<byte> ReadMappedByte() => ReadMapped(base.ReadByte);

    public SharedReader CreateDerivedAtRelativeToStartOffset(uint relativeOffset) => new((int)(_startOffset + relativeOffset), this, lockObj);
    
    public SharedReader CreateDerivedAtRelativeToCurrentOffset(uint relativeOffset) => new((int)(Offset + relativeOffset), this, lockObj);

    public override int Read(byte[] buffer, int index, int count) => throw new NotImplementedException();

    public override int Read(char[] buffer, int index, int count) => throw new NotImplementedException();

    public override int Read(Span<byte> buffer) => throw new NotImplementedException();

    public override int Read(Span<char> buffer) => throw new NotImplementedException();

    public override bool ReadBoolean() => throw new NotImplementedException();

    public override char[] ReadChars(int count) => throw new NotImplementedException();

    public override decimal ReadDecimal() => throw new NotImplementedException();

    public override double ReadDouble() => throw new NotImplementedException();

    public override Half ReadHalf() => throw new NotImplementedException();

    public override sbyte ReadSByte() => throw new NotImplementedException();

    public override float ReadSingle() => throw new NotImplementedException();

    public override string ReadString() => throw new NotImplementedException();
}