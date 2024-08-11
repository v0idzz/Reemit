using Reemit.Common;

namespace Reemit.Decompiler.Clr.Metadata.Streams;

public class BlobHeapStream(SharedReader reader, StreamHeader header)
{
    public const string Name = "#Blob";

    public byte[] Read(uint valueOffset) => ReadMapped(valueOffset);

    public RangeMapped<byte[]> ReadMapped(uint valueOffset)
    {
        if (valueOffset > header.Size)
        {
            throw new ArgumentOutOfRangeException(nameof(valueOffset), "Value offset is outside of stream");
        }

        var reader = CreateBlobReader(valueOffset, out var blobSize);
        return reader.ReadMappedBytes(blobSize);
    }

    public ConstrainedSharedReader CreateBlobReader(uint valueOffset)
    {
        var reader = CreateBlobReader(valueOffset, out var blobSize);
        return new ConstrainedSharedReader(reader.Offset, blobSize, reader);
    }

    private SharedReader CreateBlobReader(uint valueOffset, out int blobSize)
    {
        var reader1 = reader.CreateDerivedAtRelativeToStartOffset(valueOffset);

        var firstByte = reader1.ReadByte();
        if ((firstByte & 0b10000000) == 0)
        {
            blobSize = firstByte & ~0b10000000;
        }
        else if ((firstByte & 0b11000000) == 0b10000000)
        {
            var bitsEncodingSize = firstByte & ~0b11000000;
            var x = reader1.ReadByte();
            blobSize = (bitsEncodingSize << 8) + x;
        }
        else if ((firstByte & 0b11100000) == 0b11000000)
        {
            var bitsEncodingSize = firstByte & ~0b11100000;
            var bytes = reader1.ReadBytes(3);
            var x = bytes[0];
            var y = bytes[1];
            var z = bytes[2];

            blobSize = (bitsEncodingSize << 24) + (x << 16) + (y << 8) + z;
        }
        else
        {
            throw new BadImageFormatException("Blob encoding didn't match any known patterns");
        }

        return reader1;
    }
}