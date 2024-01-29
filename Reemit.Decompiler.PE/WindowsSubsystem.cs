namespace Reemit.Decompiler.PE;

public enum WindowsSubsystem : ushort
{
    ImageSubsystemUnknown = 0,
    ImageSubsystemNative = 1,
    ImageSubsystemWindowsGui = 2,
    ImageSubsystemWindowsCui = 3,
    ImageSubsystemOs2Cui = 5,
    ImageSubsystemPosixCui = 7,
    ImageSubsystemNativeWindows = 8,
    ImageSubsystemWindowsCeGui = 9,
    ImageSubsystemEfiApplication = 10,
    ImageSubsystemEfiBootServiceDriver = 11,
    ImageSubsystemEfiRuntimeDriver = 12,
    ImageSubsystemEfiRom = 13,
    ImageSubsystemXbox = 14,
    ImageSubsystemWindowsBootApplication = 16,
}