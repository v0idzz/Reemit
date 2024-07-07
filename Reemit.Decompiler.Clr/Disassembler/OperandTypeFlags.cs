namespace Reemit.Decompiler.Clr.Disassembler;

public enum OperandTypeFlags
{
    [OperandSize(0)]
    None,

    [OperandSize(8)]
    Int8,

    [OperandSize(16)]
    Int16,

    [OperandSize(32)]
    Int32,

    [OperandSize(64)]
    Int64,

    [OperandSize(8)]
    UInt8,

    [OperandSize(16)]
    UInt16,

    [OperandSize(32)]
    UInt32,
    
    [OperandSize(64)]
    UInt64,
    
    [OperandSize(32)]
    Float32,
    
    [OperandSize(64)]
    Float64,
    
    [OperandSize(32)]
    MetadataToken,

    [OperandSize(isFixedLength: false)]
    JumpTable,
}
