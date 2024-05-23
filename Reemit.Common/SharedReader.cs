namespace Reemit.Common;

// TODO: Consider allowing SharedReader to be used lock-free
public class SharedReader(int startOffset, BinaryReader reader, object lockObj) : BinaryReader(reader.BaseStream)
{
    private readonly int _startOffset = startOffset;

    public int Offset { get; private set; } = startOffset;

    public object SynchronizationObject => lockObj;

    public int RelativeOffset => Offset - _startOffset;

    public override long ReadInt64()
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(Offset, SeekOrigin.Begin);
            var value = base.ReadInt64();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            Offset += sizeof(long);
            return value;
        }
    }

    public override int ReadInt32()
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(Offset, SeekOrigin.Begin);
            var value = base.ReadInt32();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            Offset += sizeof(int);
            return value;
        }
    }
    
    public override short ReadInt16()
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(Offset, SeekOrigin.Begin);
            var value = base.ReadInt16();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            Offset += sizeof(short);
            return value;
        }
    }
    
    public override ulong ReadUInt64()
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(Offset, SeekOrigin.Begin);
            var value = base.ReadUInt64();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            Offset += sizeof(ulong);
            return value;
        }
    }

    public override uint ReadUInt32()
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(Offset, SeekOrigin.Begin);
            var value = base.ReadUInt32();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            Offset += sizeof(uint);
            return value;
        }
    }
    
    public override ushort ReadUInt16()
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(Offset, SeekOrigin.Begin);
            var value = base.ReadUInt16();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            Offset += sizeof(ushort);
            return value;
        }
    }

    public override byte[] ReadBytes(int count)
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(Offset, SeekOrigin.Begin);
            var value = base.ReadBytes(count);
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            Offset += sizeof(byte) * count;
            return value;
        }
    }

    public override char ReadChar()
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(Offset, SeekOrigin.Begin);
            var value = base.ReadChar();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            Offset += sizeof(char);
            return value;
        }
    }

    public override byte ReadByte()
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(Offset, SeekOrigin.Begin);
            var value = base.ReadByte();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            Offset += sizeof(byte);
            return value;
        }
    }

    public SharedReader CreateDerivedAtRelativeOffset(uint relativeOffset) => new((int)(_startOffset + relativeOffset), this, lockObj);
}