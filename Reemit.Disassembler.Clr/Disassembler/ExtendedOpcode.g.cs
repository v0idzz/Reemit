namespace Reemit.Disassembler.Clr.Disassembler;

public enum ExtendedOpcode : byte
{
    [Mnemonic("arglist", true)]
    @arglist = 0x00,

    [Mnemonic("ceq", true)]
    @ceq = 0x01,

    [Mnemonic("cgt", true)]
    @cgt = 0x02,

    [Mnemonic("cgt.un", true)]
    @cgt_un = 0x03,

    [Mnemonic("clt", true)]
    @clt = 0x04,

    [Mnemonic("clt.un", true)]
    @clt_un = 0x05,

    [Mnemonic("ldftn", true)]
    @ldftn = 0x06,

    [Mnemonic("ldvirtftn", true)]
    @ldvirtftn = 0x07,

    [Mnemonic("ldarg", true)]
    @ldarg = 0x09,

    [Mnemonic("ldarga", true)]
    @ldarga = 0x0A,

    [Mnemonic("starg", true)]
    @starg = 0x0B,

    [Mnemonic("ldloc", true)]
    @ldloc = 0x0C,

    [Mnemonic("ldloca", true)]
    @ldloca = 0x0D,

    [Mnemonic("stloc", true)]
    @stloc = 0x0E,

    [Mnemonic("localloc", true)]
    @localloc = 0x0F,

    [Mnemonic("endfilter", true)]
    @endfilter = 0x11,

    [Mnemonic("unaligned.", true)]
    @unaligned = 0x12,

    [Mnemonic("volatile.", true)]
    @volatile = 0x13,

    [Mnemonic("tail.", true)]
    @tail = 0x14,

    [Mnemonic("initobj", true)]
    @initobj = 0x15,

    [Mnemonic("constrained.", true)]
    @constrained = 0x16,

    [Mnemonic("cpblk", true)]
    @cpblk = 0x17,

    [Mnemonic("initblk", true)]
    @initblk = 0x18,

    [Mnemonic("no.", true)]
    @no = 0x19,

    [Mnemonic("rethrow", true)]
    @rethrow = 0x1A,

    [Mnemonic("sizeof", true)]
    @sizeof = 0x1C,

    [Mnemonic("refanytype", true)]
    @refanytype = 0x1D,

    [Mnemonic("readonly.", true)]
    @readonly = 0x1E,

    None = 0xFF,
}

