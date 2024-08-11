namespace Reemit.Decompiler.Clr.Signatures.Types;

// A subset of ELEMENT_TYPE constants (defined in ECMA-335 II.23.1.16) that are used to indicate GenericInst signature
// type.
public enum GenericInstSigTypeKind : byte
{
    ValueType = 0x11,
    Class = 0x12
}