namespace Reemit.Decompiler.Clr.Signatures;

public enum ElementType : byte
{
    End = 0x00,
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
    String = 0x0e,
    Ptr = 0x0f,
    ByRef = 0x10,
    ValueType = 0x11,
    Class = 0x12,
    Var = 0x13,
    Array = 0x14,
    GenericInst = 0x15,
    TypedByRef = 0x16,
    I = 0x18,
    U = 0x19,
    FnPtr = 0x1b,
    Object = 0x1c,
    SZArray = 0x1d,
    MVar = 0x1e,
    CmodReqd = 0x1f,
    CmodOpt = 0x20,
    Internal = 0x21,
    Modifier = 0x40,
    Sentinel = 0x41,
    Pinned = 0x45,
    Unnamed1 = 0x50,
    Unnamed2 = 0x51,
    Reserved = 0x52,
    Unnamed3 = 0x53,
    Unnamed4 = 0x54,
    Unnamed5 = 0x55
}