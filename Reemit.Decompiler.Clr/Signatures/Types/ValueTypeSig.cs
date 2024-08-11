using Reemit.Common;

namespace Reemit.Decompiler.Clr.Signatures.Types;

public record ValueTypeSig(TypeDefOrRefOrSpecEncodedSig TypeDefOrRefOrSpecEncoded) : ITypeSig
{
    public static ValueTypeSig Read(SharedReader reader)
    {
        var typeDefOrRefOrSpecEncoded = TypeDefOrRefOrSpecEncodedSig.Read(reader);
        return new ValueTypeSig(typeDefOrRefOrSpecEncoded);
    }
}