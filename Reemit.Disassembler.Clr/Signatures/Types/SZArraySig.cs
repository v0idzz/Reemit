using Reemit.Common;

namespace Reemit.Disassembler.Clr.Signatures.Types;

public record SZArraySig(IReadOnlyList<CustomModSig> CustomMods, ITypeSig Type) : ITypeSig
{
    public static SZArraySig Read(ConstrainedSharedReader reader)
    {
        var customMods = CustomModSig.ReadAll(reader);
        var type = TypeSigReader.ReadType(reader);

        return new SZArraySig(customMods, type);
    }
}