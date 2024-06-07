namespace Reemit.Decompiler.Clr.Metadata;

/// <summary>
/// Based on II.23.1.15 Flags for types [TypeAttributes] 
/// </summary>
public enum TypeVisibilityAttributes : ushort
{
    NotPublic = 0x0,
    Public = 0x1,
    NestedPublic = 0x2,
    NestedPrivate = 0x3,
    NestedFamily = 0x4,
    NestedAssembly = 0x5,
    NestedFamAndAssem = 0x6,
    NestedFamOrAssem = 0x7,
    
    Mask =
        NotPublic |
        Public |
        NestedPublic |
        NestedPrivate |
        NestedFamily |
        NestedAssembly |
        NestedFamAndAssem |
        NestedFamOrAssem
}