namespace Reemit.Decompiler.Clr;

public enum NativeType : byte
{
    Void = 0x01,
    Boolean = 0x02,
    Char = 0x03,
    I1 = 0x04,
    U1 = 0x05,
    I2 = 0x06,
    U2 = 0x07,
    I4 = 0x08,
    U4 = 0x09,
    I8 = 0x0a,
    U8 = 0x0b,
    R4 = 0x0c,
    R8 = 0x0d,
    I = 0x18,
    U = 0x19,
    String = 0x0e,
    TypedByRef = 0x16,
    Object = 0x1c,
}