namespace Reemit.Decompiler.Cli.Metadata;

public enum TypeAttributes : uint
{
    NotPublic = 0x0,
    Public = 0x1,
    NestedPublic = 0x2,
    NestedPrivate = 0x3,
    NestedFamily = 0x4,
    NestedAssembly = 0x5,
    NestedFamAndAssem = 0x6,
    NestedFamOrAssem = 0x7
}