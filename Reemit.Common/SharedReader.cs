namespace Reemit.Common;

// TODO: Consider allowing SharedReader to be used lock-free
public class SharedReader(int startOffset, BinaryReader reader, object lockObj) : BinaryReader(reader.BaseStream)
{
    private readonly int _startOffset = startOffset;

    public int Offset { get; private set; } = startOffset;

    public object SynchronizationObject => lockObj;

    public int RelativeOffset => Offset - _startOffset;

    private T Read<T>(Func<T> readFunc)
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(Offset, SeekOrigin.Begin);
            var value = readFunc();
            var size = BaseStream.Position - Offset;
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            Offset += (int)size;

            return value;
        }
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

    public SharedReader CreateDerivedAtRelativeOffset(uint relativeOffset) => new((int)(_startOffset + relativeOffset), this, lockObj);

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