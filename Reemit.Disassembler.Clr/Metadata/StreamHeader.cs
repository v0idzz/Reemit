using System.Text;

namespace Reemit.Disassembler.Clr.Metadata;

public record StreamHeader(uint Offset, uint Size, string Name)
{
    public static StreamHeader Read(BinaryReader reader)
    {
        var offset = reader.ReadUInt32();
        var size = reader.ReadUInt32();
        
        // ECMA-335 §II.24.2.2 
        if (size % 4 != 0)
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

        var name = Encoding.ASCII.GetString(nameBytes.ToArray());
        return new StreamHeader(offset, size, name);
    }
}