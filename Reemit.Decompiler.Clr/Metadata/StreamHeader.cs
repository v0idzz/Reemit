using System.Text;

namespace Reemit.Decompiler.Cli.Metadata;

public class StreamHeader
{
    public uint Offset { get; }
    public uint Size { get; }
    public string Name { get; }

    public StreamHeader(BinaryReader reader)
    {
        Offset = reader.ReadUInt32();
        Size = reader.ReadUInt32();
        
        // ECMA-335 §II.24.2.2 
        if (Size % 4 != 0)
        {
            throw new BadImageFormatException("Size of stream shall be a multiple of 4.");
        }

        // ECMA-335 §II.24.2.2
        // "Name (...) padded to the next 4-byte boundary"
        const int byteBoundary = 4;
        var nameBytes = new List<byte>(byteBoundary);

        while (true)
        {
            var buffer = reader.ReadBytes(byteBoundary);
            var indexOfNullChar = Array.IndexOf(buffer, (byte)'\0');

            if (indexOfNullChar == -1)
            {
                nameBytes.AddRange(buffer);
                continue;
            }
            
            nameBytes.AddRange(buffer[..indexOfNullChar]);
            break;
        }

        Name = Encoding.ASCII.GetString(nameBytes.ToArray());
    }
}