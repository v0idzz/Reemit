namespace Reemit.Decompiler.Clr.Metadata;

public enum TypeAttributes : uint
{
    Abstract = 0x80,
    Sealed = 0x100,
    SpecialName = 0x400,
    Import = 0x1000,
    Serializable = 0x2000,
    BeforeFieldInit = 0x100000,
    RTSpecialName = 0x800,
    HasSecurity = 0x40000,
    IsTypeForwarder = 0x200000,
    
    Mask = 
        Abstract |
        Sealed |
        SpecialName |
        Import |
        Serializable |
        BeforeFieldInit |
        RTSpecialName |
        HasSecurity |
        IsTypeForwarder
}