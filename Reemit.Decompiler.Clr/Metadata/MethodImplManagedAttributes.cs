namespace Reemit.Decompiler.Clr.Metadata;

/// <summary>
/// Based on 23.1.11 Flags for methods [MethodImplAttributes]
/// 
/// Note:
/// Not decorated with FlagsAttribute as these are not
/// flags in the .NET sense.
/// </summary>
public enum MethodImplManagedAttributes : ushort
{
    Unmanaged = 0x0004,
    Managed = 0x0000,

    Mask =
        Unmanaged |
        Managed
}
