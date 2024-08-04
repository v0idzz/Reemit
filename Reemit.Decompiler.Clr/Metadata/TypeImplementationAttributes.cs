namespace Reemit.Decompiler.Clr.Metadata;

/// <summary>
/// Based on II.23.1.15 Flags for types [TypeAttributes] 
/// </summary>
public enum TypeImplementationAttributes : ushort
{
    Import = 0x1000,
    Serializable = 0x2000,
    WindowsRuntime = 0x4000,

    Mask = 
        Import |
        Serializable |
        WindowsRuntime
}