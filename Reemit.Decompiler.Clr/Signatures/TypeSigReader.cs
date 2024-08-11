using Reemit.Common;
using Reemit.Decompiler.Clr.Metadata.Streams;
using Reemit.Decompiler.Clr.Signatures.Types;

namespace Reemit.Decompiler.Clr.Signatures;

public static class TypeSigReader
{
    public static ITypeSig ReadType(ConstrainedSharedReader reader)
    {
        var type = (ElementType)reader.ReadSignatureUInt();

        switch (type)
        {
            case ElementType.Array:
                return ArraySig.Read(reader);
            case ElementType.Class:
                return ClassSig.Read(reader);
            case ElementType.FnPtr:
                return FnPtrSig.Read(reader);
            case ElementType.GenericInst:
                return GenericInstSig.Read(reader);
            case ElementType.MVar:
                return MVarSig.Read(reader);
            case ElementType.Ptr:
                return PtrSig.Read(reader);
            case ElementType.SZArray:
                return SZArraySig.Read(reader);
            case ElementType.ValueType:
                return ValueTypeSig.Read(reader);
            case ElementType.Var:
                return VarSig.Read(reader);
        }

        var nativeType = (NativeType)type;
        
        if (Enum.IsDefined(nativeType))
        {
            return new NativeTypeSig(nativeType);
        }

        throw new BadImageFormatException("Unsupported ElementType");
    }
}