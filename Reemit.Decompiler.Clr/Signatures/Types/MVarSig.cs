using Reemit.Common;
using Reemit.Decompiler.Clr.Metadata.Streams;

namespace Reemit.Decompiler.Clr.Signatures.Types;

public record MVarSig(uint Number) : ITypeSig
{
    public static MVarSig Read(SharedReader reader) => new(reader.ReadSignatureUInt());
}