using Reemit.Common;
using Reemit.Decompiler.Clr.Metadata.Streams;

namespace Reemit.Decompiler.Clr.Signatures.Types;

public record GenericInstSig(
    GenericInstSigTypeKind TypeKind,
    TypeDefOrRefOrSpecEncodedSig Type,
    IReadOnlyList<ParamSig> GenericArguments) : ITypeSig
{
    public static GenericInstSig Read(ConstrainedSharedReader reader)
    {
        var typeKind = (GenericInstSigTypeKind)reader.ReadSignatureUInt();

        if (!Enum.IsDefined(typeKind))
        {
            throw new BadImageFormatException("Unexpected type kind in GenericInst signature");
        }

        var type = TypeDefOrRefOrSpecEncodedSig.Read(reader);

        var genArgCount = (int)reader.ReadSignatureUInt();
        var genericArguments = new List<ParamSig>(genArgCount);
        for (var i = 0; i < genArgCount; i++)
        {
            var param = ParamSig.Read(reader);
            genericArguments.Add(param);
        }

        return new GenericInstSig(typeKind, type, genericArguments);
    }
}