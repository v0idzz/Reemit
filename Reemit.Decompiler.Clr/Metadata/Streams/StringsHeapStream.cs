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

    public string Read(uint valueOffset) => ReadMapped(valueOffset);

    public RangeMapped<string> ReadMapped(uint valueOffset)
    {
        var reader = _reader.CreateDerivedAtRelativeOffset(valueOffset);

        const int bufferSize = 16;
        var valBytes = new List<byte>(bufferSize);
        var rangePosition = reader.Offset;
        var rangeLength = 0;

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
                rangeLength += buffer.Length;
                valBytes.AddRange(buffer);
                continue;
            }

            rangeLength += indexOfNullChar;

            if (buffer.Length < nextBufferSize)
            {
                throw new ArgumentException("End of stream reached without encountering null character",
                    nameof(valueOffset));
            }

            valBytes.AddRange(buffer[..indexOfNullChar]);
            break;
        }

        return new RangeMapped<string>(rangePosition, rangeLength, Encoding.UTF8.GetString(valBytes.ToArray()));
    }
}