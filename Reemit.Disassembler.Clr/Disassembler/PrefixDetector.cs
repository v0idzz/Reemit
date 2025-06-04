namespace Reemit.Disassembler.Clr.Disassembler;

public static class PrefixDetector
{
    public static bool IsPrefix(ExtendedOpcode extendedOpcode) =>
        extendedOpcode switch
        {
            ExtendedOpcode.constrained or
            ExtendedOpcode.no or
            ExtendedOpcode.@readonly or
            ExtendedOpcode.tail or
            ExtendedOpcode.unaligned or
            ExtendedOpcode.@volatile => true,
            _ => false,
        };
}
