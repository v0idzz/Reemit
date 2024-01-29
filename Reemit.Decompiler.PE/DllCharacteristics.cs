namespace Reemit.Decompiler.PE;

[Flags]
public enum DllCharacteristics : ushort
{
    Reserved1 = 0x0001,
    Reserved2 = 0x0002,
    Reserved3 = 0x0004,
    Reserved4 = 0x0008,
    ImageDllCharacteristicsHighEntropyVa = 0x0020,
    ImageDllCharacteristicsDynamicBase = 0x0040,
    ImageDllCharacteristicsForceIntegrity = 0x0080,
    ImageDllCharacteristicsNxCompat = 0x0100,
    ImageDllCharacteristicsNoIsolation = 0x0200,
    ImageDllCharacteristicsNoSeh = 0x0400,
    ImageDllCharacteristicsNoBind = 0x0800,
    ImageDllCharacteristicsAppContainer = 0x1000,
    ImageDllCharacteristicsWdmDriver = 0x2000,
    ImageDllCharacteristicsGuardCf = 0x4000,
    ImageDllCharacteristicsTerminalServerAware = 0x8000,
}