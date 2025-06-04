namespace Reemit.Disassembler.Clr.Signatures;

public record GenericMethodSig(
    MethodDefSigCallingConvention CallingConvention,
    RetTypeSig RetType,
    uint GenParamCount,
    IReadOnlyList<ParamSig> Params) : MethodSig(CallingConvention, RetType, Params);