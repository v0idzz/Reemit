namespace Reemit.Decompiler.Clr.Metadata;

/// <summary>
/// Based on 23.1.11 Flags for methods [MethodImplAttributes]
/// </summary>
[Flags]
public enum MethodImplAttributes : ushort
{
    ForwardRef = 0x0010,
    PreserveSig = 0x0080,
    InternalCall = 0x1000,
    Synchronized = 0x0020,
    NoInlining = 0x0008,
    // Doesn't quite work with FlagsAttribute, but keeping
    // it marked as such anyway.
    MaxMethodImplVal = 0xffff,
    NoOptimization = 0x0040,

    // Omitting MaxMethodImplVal from mask
    Mask =
        ForwardRef |
        PreserveSig |
        InternalCall |
        NoInlining |
        NoOptimization
}