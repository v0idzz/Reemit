using System.Text;
using Reemit.Common;

namespace Reemit.Disassembler.Clr.Metadata.Streams;

public class UserStringsHeapStream(SharedReader reader, StreamHeader header)
{
    public const string Name = "#US";

    private readonly BlobHeapStream _blobHeapStream = new(reader, header);

    public string ReadString(uint valueOffset)
    {
        var bytes = _blobHeapStream.Read(valueOffset);
        return Encoding.Unicode.GetString(bytes.AsSpan()[..^1]);
    }
}