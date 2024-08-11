using Reemit.Common;
using Reemit.Decompiler.Clr.Metadata.Streams;

namespace Reemit.Decompiler.Clr.Signatures;

public record MethodSig(
    MethodDefSigCallingConvention CallingConvention,
    RetTypeSig RetType,
    IReadOnlyList<ParamSig> Params,
    IReadOnlyList<ParamSig>? VarArgParams = null)
{
    public static MethodSig Read(ConstrainedSharedReader reader)
    {
        var callingConvention = (MethodDefSigCallingConvention)reader.ReadByte();
        uint? genParamCount = null;

        if (callingConvention.HasFlag(MethodDefSigCallingConvention.Generic))
        {
            genParamCount = reader.ReadSignatureUInt();
        }

        var paramCount = (int)reader.ReadSignatureUInt();
        var retType = RetTypeSig.Read(reader);

        var @params = new List<ParamSig>(paramCount);
        for (var i = 0; i < paramCount; i++)
        {
            var param = ParamSig.Read(reader);
            @params.Add(param);
        }

        // If the calling convention is not VarArg, then the signature has to be MethodDefSig, which doesn't have any
        // data further.
        if (!callingConvention.HasFlag(MethodDefSigCallingConvention.VarArg) || reader.IsEndOfStream)
        {
            return genParamCount is null
                ? new MethodSig(callingConvention, retType, @params)
                : new GenericMethodSig(callingConvention, retType, genParamCount.Value, @params);
        }
        
        // If we haven't reached end of stream by now, the signature is a MethodRefSig in a case where it differs from
        // MethodDefSig.
        var varArgParams = new List<ParamSig>();

        for (var i = 0; i < paramCount; i++)
        {
            if ((ElementType)reader.ReadSignatureUInt() is not ElementType.Sentinel)
            {
                throw new BadImageFormatException("Bad MethodRefSig, expected Sentinel element type");
            }

            var param = ParamSig.Read(reader);
            varArgParams.Add(param);
        }

        return new MethodSig(callingConvention, retType, @params, varArgParams);
    }
}