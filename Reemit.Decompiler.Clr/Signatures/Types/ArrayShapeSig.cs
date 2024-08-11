using Reemit.Common;
using Reemit.Decompiler.Clr.Metadata.Streams;

namespace Reemit.Decompiler.Clr.Signatures.Types;

public record ArrayShapeSig(uint Rank, IReadOnlyList<uint> Sizes, IReadOnlyList<uint> LoBounds)
{
    public static ArrayShapeSig Read(SharedReader reader)
    {
        var rank = reader.ReadSignatureUInt();

        var numSizes = (int)reader.ReadSignatureUInt();
        var sizes = new List<uint>(numSizes);
        for (var i = 0; i < numSizes; i++)
        {
            var size = reader.ReadSignatureUInt();
            sizes.Add(size);
        }

        var numLoBounds = (int)reader.ReadSignatureUInt();
        var loBounds = new List<uint>(numLoBounds);
        for (var i = 0; i < numLoBounds; i++)
        {
            var loBound = reader.ReadSignatureUInt();
            loBounds.Add(loBound);
        }

        return new ArrayShapeSig(rank, sizes, loBounds);
    }
}