using Reemit.Common;
using Reemit.Disassembler.Clr.Metadata.Streams;

namespace Reemit.Disassembler.Clr.Signatures.Types;

public record MVarSig(uint Number) : ITypeSig
{
    public static MVarSig Read(SharedReader reader) => new(reader.ReadSignatureUInt());
}