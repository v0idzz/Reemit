using Reemit.Common;

namespace Reemit.Decompiler.Clr.Signatures.Types;

public record ClassSig(TypeDefOrRefOrSpecEncodedSig TypeDefOrRefOrSpecEncoded) : ITypeSig
{
    public static ClassSig Read(SharedReader reader)
    {
        var typeDefOrRefOrSpecEncoded = TypeDefOrRefOrSpecEncodedSig.Read(reader);
        return new ClassSig(typeDefOrRefOrSpecEncoded);
    }
}