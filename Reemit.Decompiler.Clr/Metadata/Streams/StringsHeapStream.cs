using System.Text;
using Reemit.Common;

namespace Reemit.Decompiler.Clr.Metadata.Streams;

public class StringsHeapStream
{
    public const string Name = "#Strings";

    private readonly SharedReader _reader;
    private readonly StreamHeader _header;

    public StringsHeapStream(SharedReader reader, StreamHeader header)
    {
        _reader = reader;
        _header = header;
    }

    public string Read(uint valueOffset)
    {
        var reader = _reader.CreateDerivedAtRelativeOffset(valueOffset);

        const int bufferSize = 16;
        var valBytes = new List<byte>(bufferSize);

        while (true)
        {
            // The _reader field is fixed at the beginning of the stream while the local reader variable is pointing
            // at the address we are currently reading.
            var positionIntoStream = reader.Offset - _reader.Offset;
            var nextBufferSize = Math.Min(bufferSize, _header.Size - positionIntoStream);

            if (nextBufferSize <= 0)
            {
                throw new ArgumentException("End of stream reached without encountering null character",
                    nameof(valueOffset));
            }
            
            var buffer = reader.ReadBytes((int) nextBufferSize);
            
            var indexOfNullChar = Array.IndexOf(buffer, (byte)'\0');

            if (indexOfNullChar == -1)
            {
                valBytes.AddRange(buffer);
                continue;
            }

            if (buffer.Length < nextBufferSize)
            {
                throw new ArgumentException("End of stream reached without encountering null character",
                    nameof(valueOffset));
            }

            valBytes.AddRange(buffer[..indexOfNullChar]);
            break;
        }

        return Encoding.UTF8.GetString(valBytes.ToArray());
    }
}