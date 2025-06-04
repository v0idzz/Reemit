using Reemit.Common;
using Reemit.Disassembler.Clr.Metadata.Streams;

namespace Reemit.Disassembler.Clr.Signatures.Types;

public record VarSig(uint Number) : ITypeSig
{
    public static VarSig Read(SharedReader reader) => new(reader.ReadSignatureUInt());
}