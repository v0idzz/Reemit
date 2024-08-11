namespace Reemit.Decompiler.Clr.Signatures;

[Flags]
public enum MethodDefSigCallingConvention : byte
{
    Default = 0x0,
    VarArg = 0x5,
    Generic = 0x10,
    HasThis = 0x20,
    ExplicitThis = 0x40
}