namespace Reemit.Decompiler.Clr.Metadata;

/// <summary>
/// Based on 23.1.10 Flags for methods [MethodAttributes]
/// </summary>
[Flags]
public enum MethodAttributes : ushort
{
    Static = 0x0010,
    Final = 0x0020,
    Virtual = 0x0040,
    HideBySig = 0x0080,
    Strict = 0x0200,
    Abstract = 0x0400,
    SpecialName = 0x0800,
    PInvokeImpl = 0x2000,
    UnmanagedExport = 0x0008,
    RTSpecialName = 0x1000,
    HasSecurity = 0x4000,
    RequireSecObject = 0x8000,

    Mask =
        Static |
        Final |
        Virtual |
        HideBySig |
        Strict |
        Abstract |
        SpecialName |
        PInvokeImpl |
        UnmanagedExport |
        RTSpecialName |
        HasSecurity |
        RequireSecObject
}
