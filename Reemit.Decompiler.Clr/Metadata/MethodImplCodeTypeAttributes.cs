namespace Reemit.Decompiler.Clr.Metadata;

/// <summary>
/// Based on 23.1.11 Flags for methods [MethodImplAttributes]
/// 
/// Note:
/// Not decorated with FlagsAttribute as these are not
/// flags in the .NET sense.
/// </summary>
public enum MethodImplCodeTypeAttributes : ushort
{
    IL = 0x0000,
    Native = 0x0001,
    OPTIL = 0x0002,
    Runtime = 0x0003,

    Mask =
        IL |
        Native |
        OPTIL |
        Runtime
}
