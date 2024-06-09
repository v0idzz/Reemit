namespace Reemit.Decompiler.Clr.Metadata;

/// <summary>
/// Based on II.23.1.15 Flags for types [TypeAttributes] 
/// </summary>
public enum TypeClassSemanticsAttributes : ushort
{
    Class = 0x0,
    Interface = 0x20,
    
    Mask =
        Class |
        Interface
}