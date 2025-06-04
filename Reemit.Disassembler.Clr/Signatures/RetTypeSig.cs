using Reemit.Common;
using Reemit.Disassembler.Clr.Metadata.Streams;
using Reemit.Disassembler.Clr.Signatures.Types;

namespace Reemit.Disassembler.Clr.Signatures;

public record RetTypeSig(IReadOnlyList<CustomModSig> CustomMods, ITypeSig? Type, bool ByRef = false)
{
    public static RetTypeSig Read(ConstrainedSharedReader reader)
    {
        var customMods = CustomModSig.ReadAll(reader);

        var lookAheadReader = reader.CreateDerivedAtRelativeToCurrentOffset(0);
        var value = (ElementType)lookAheadReader.ReadSignatureUInt();

        if (value is ElementType.ByRef or ElementType.TypedByRef or ElementType.Void)
        {
            // offset the original reader
            reader.ReadSignatureUInt();

            if (value == ElementType.ByRef)
            {
                return new RetTypeSig(customMods, TypeSigReader.ReadType(reader), true);
            }

            if (value == ElementType.Void)
            {
                return new RetTypeSig(customMods, new NativeTypeSig(NativeType.Void));
            }

            return new RetTypeSig(customMods, new NativeTypeSig(NativeType.TypedByRef));
        }

        return new RetTypeSig(customMods, TypeSigReader.ReadType(reader));
    }
}