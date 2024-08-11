using Reemit.Common;

namespace Reemit.Decompiler.Clr.Signatures.Types;

public record ArraySig(ITypeSig Type, ArrayShapeSig Shape) : ITypeSig
{
    public static ArraySig Read(ConstrainedSharedReader reader)
    {
        var type = TypeSigReader.ReadType(reader);
        var shape = ArrayShapeSig.Read(reader);

        return new ArraySig(type, shape);
    }
}