using System.Runtime.InteropServices;

namespace Reemit.Common;

// TODO: Consider allowing SharedReader to be used lock-free
public class SharedReader(int startOffset, BinaryReader reader, object lockObj) : BinaryReader(reader.BaseStream)
{
    private readonly int _startOffset = startOffset;

    public int Offset { get; private set; } = startOffset;

    public object SynchronizationObject => lockObj;

    public int RelativeOffset => Offset - _startOffset;

    private T ReadUnmanaged<T>(Func<T> readFunc)
        where T : unmanaged =>
        ReadUnmanaged(readFunc, Marshal.SizeOf<T>);

    private T ReadUnmanaged<T>(Func<T> readFunc, Func<int> getSizeFunc)
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(Offset, SeekOrigin.Begin);
            var value = readFunc();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            Offset += getSizeFunc();
            return value;
        }
    }

    public override long ReadInt64() => ReadUnmanaged(base.ReadInt64);

    public override int ReadInt32() => ReadUnmanaged(base.ReadInt32);
    
    public override short ReadInt16() => ReadUnmanaged(base.ReadInt16);
    
    public override ulong ReadUInt64() => ReadUnmanaged(base.ReadUInt64);

    public override uint ReadUInt32() => ReadUnmanaged(base.ReadUInt32);
    
    public override ushort ReadUInt16() => ReadUnmanaged(base.ReadUInt16);

    public override byte[] ReadBytes(int count) =>
        ReadUnmanaged(() => base.ReadBytes(count), () => sizeof(byte) * count);

    public override char ReadChar() => ReadUnmanaged(base.ReadChar);

    public override byte ReadByte() => ReadUnmanaged(base.ReadByte);

    public SharedReader CreateDerivedAtRelativeOffset(uint relativeOffset) => new((int)(_startOffset + relativeOffset), this, lockObj);
}