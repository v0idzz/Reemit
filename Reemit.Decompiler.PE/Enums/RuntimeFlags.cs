namespace Reemit.Decompiler.PE.Enums;

public enum RuntimeFlags : uint
{
    ILOnly = 0x00000001,
    Required32Bit = 0x00000002,
    StrongNameSigned = 0x00000008,
    NativeEntryPoint = 0x00000010,
    TrackDebugData = 0x00010000,
}