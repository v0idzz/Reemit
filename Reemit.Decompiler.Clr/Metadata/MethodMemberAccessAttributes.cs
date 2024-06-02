namespace Reemit.Decompiler.Clr.Metadata;

/// <summary>
/// Based on 23.1.10 Flags for methods [MethodAttributes]
/// 
/// Note:
/// Not decorated with FlagsAttribute as these are not
/// flags in the .NET sense.
/// </summary>
public enum MethodMemberAccessAttributes : ushort
{
    CompilerControlled = 0x0000,
    Private = 0x0001,
    FamilyAndAssembly = 0x0002,
    Assembly = 0x0003,
    Family = 0x0004,
    FamilyOrAssembly = 0x0005,
    Public = 0x0006,
    
    Mask =
        CompilerControlled |
        Private |
        FamilyAndAssembly |
        Assembly |
        Family |
        FamilyOrAssembly |
        Public
}
