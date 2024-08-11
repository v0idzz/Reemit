using Reemit.Common;
using Reemit.Decompiler.Clr.Metadata.Streams;

namespace Reemit.Decompiler.Clr.Signatures.Types;

public record VarSig(uint Number) : ITypeSig
{
    public static VarSig Read(SharedReader reader) => new(reader.ReadSignatureUInt());
}