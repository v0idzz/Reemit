using Reemit.Common;
using Reemit.Decompiler.Clr.Metadata.Streams;
using Reemit.Decompiler.Clr.Signatures.Types;

namespace Reemit.Decompiler.Clr.Signatures;

public record ParamSig(IReadOnlyList<CustomModSig> CustomMods, ITypeSig? Type, bool ByRef = false)
{
    public static ParamSig Read(ConstrainedSharedReader reader)
    {
        var customMods = CustomModSig.ReadAll(reader);

        var lookAheadReader = reader.CreateDerivedAtRelativeToCurrentOffset(0);
        var value = (ElementType)lookAheadReader.ReadSignatureUInt();

        if (value is ElementType.ByRef or ElementType.TypedByRef)
        {
            // offset the original reader
            reader.ReadSignatureUInt();

            return value == ElementType.ByRef
                ? new ParamSig(customMods, TypeSigReader.ReadType(reader), true)
                : new ParamSig(customMods, new NativeTypeSig(NativeType.TypedByRef));
        }

        return new ParamSig(customMods, TypeSigReader.ReadType(reader));
    }
}