using Reemit.Common;
using Reemit.Decompiler.Clr.Metadata.Streams;

namespace Reemit.Decompiler.Clr.Signatures;

public record TypeDefOrRefOrSpecEncodedSig(TypeDefOrRefOrSpec TypeDefOrRefOrSpec, uint RowIndex)
{
    public static TypeDefOrRefOrSpecEncodedSig Read(SharedReader reader)
    {
        var rawValue = reader.ReadSignatureUInt();
        var rowIndex = (rawValue & ~(uint)TypeDefOrRefOrSpec.Mask) >> 2;
        var referencedTable = (TypeDefOrRefOrSpec)(rawValue & (uint)TypeDefOrRefOrSpec.Mask);

        return new TypeDefOrRefOrSpecEncodedSig(referencedTable, rowIndex);
    }
}