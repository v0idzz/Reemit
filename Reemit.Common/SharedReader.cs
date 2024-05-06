namespace Reemit.Common;

public sealed class SharedReader(int offset, BinaryReader reader, object lockObj) : BinaryReader(reader.BaseStream)
{
    public override long ReadInt64()
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(offset, SeekOrigin.Begin);
            var value = base.ReadInt64();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            offset += sizeof(long);
            return value;
        }
    }

    public override int ReadInt32()
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(offset, SeekOrigin.Begin);
            var value = base.ReadInt32();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            offset += sizeof(int);
            return value;
        }
    }
    
    public override short ReadInt16()
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(offset, SeekOrigin.Begin);
            var value = base.ReadInt16();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            offset += sizeof(short);
            return value;
        }
    }
    
    public override ulong ReadUInt64()
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(offset, SeekOrigin.Begin);
            var value = base.ReadUInt64();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            offset += sizeof(ulong);
            return value;
        }
    }

    public override uint ReadUInt32()
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(offset, SeekOrigin.Begin);
            var value = base.ReadUInt32();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            offset += sizeof(uint);
            return value;
        }
    }
    
    public override ushort ReadUInt16()
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(offset, SeekOrigin.Begin);
            var value = base.ReadUInt16();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            offset += sizeof(ushort);
            return value;
        }
    }

    public override byte[] ReadBytes(int count)
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(offset, SeekOrigin.Begin);
            var value = base.ReadBytes(count);
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            offset += sizeof(byte) * count;
            return value;
        }
    }

    public override char ReadChar()
    {
        lock (lockObj)
        {
            var offsetCopy = BaseStream.Position;
            BaseStream.Seek(offset, SeekOrigin.Begin);
            var value = base.ReadChar();
            BaseStream.Seek(offsetCopy, SeekOrigin.Begin);
            offset += sizeof(char);
            return value;
        }
    }
}