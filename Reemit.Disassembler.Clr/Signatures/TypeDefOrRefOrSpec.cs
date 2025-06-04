namespace Reemit.Disassembler.Clr.Signatures;

// Contains all tokens that can be referenced by [II.23.2.8] TypeDefOrRefOrSpecEncoded.
// The values correspond to bits used to encode the indexed table in TypeDefOrRefOrSpecEncoded signature item
public enum TypeDefOrRefOrSpec : uint
{
    TypeDef = 0x0,
    TypeRef = 0x1,
    TypeSpec = 0x2,
    
    Mask =
        TypeDef |
        TypeRef |
        TypeSpec
}