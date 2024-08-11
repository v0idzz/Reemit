using Reemit.Common;

namespace Reemit.Decompiler.Clr.Signatures.Types;

public record PtrSig(IReadOnlyList<CustomModSig> CustomMods, ITypeSig Type) : ITypeSig
{
    public static PtrSig Read(ConstrainedSharedReader reader) =>
        new(CustomModSig.ReadAll(reader), TypeSigReader.ReadType(reader));
}