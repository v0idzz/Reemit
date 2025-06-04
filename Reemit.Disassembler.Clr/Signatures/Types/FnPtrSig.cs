using Reemit.Common;

namespace Reemit.Disassembler.Clr.Signatures.Types;

public record FnPtrSig(MethodSig MethodSig) : ITypeSig
{
    public static FnPtrSig Read(ConstrainedSharedReader reader) => new(MethodSig.Read(reader));
}