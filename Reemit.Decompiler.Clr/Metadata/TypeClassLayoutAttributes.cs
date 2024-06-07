namespace Reemit.Decompiler.Clr.Metadata;

/// <summary>
/// Based on II.23.1.15 Flags for types [TypeAttributes] 
/// </summary>
public enum TypeClassLayoutAttributes : uint
{
    AutoLayout = 0x0,
    SequentialLayout = 0x8,
    ExplicitLayout = 0x10,
    
    Mask = 
        AutoLayout |
        SequentialLayout |
        ExplicitLayout
}