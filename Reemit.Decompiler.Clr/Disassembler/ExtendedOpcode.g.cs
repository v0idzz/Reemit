namespace Reemit.Decompiler.Clr.Disassembler;

public enum ExtendedOpcode : byte
{
    [CilMnemonic("arglist")]
    @arglist = 0x00,
    [CilMnemonic("ceq")]
    @ceq = 0x01,
    [CilMnemonic("cgt")]
    @cgt = 0x02,
    [CilMnemonic("cgt.un")]
    @cgt_un = 0x03,
    [CilMnemonic("clt")]
    @clt = 0x04,
    [CilMnemonic("clt.un")]
    @clt_un = 0x05,
    [CilMnemonic("ldftn")]
    @ldftn = 0x06,
    [CilMnemonic("ldvirtftn")]
    @ldvirtftn = 0x07,
    [CilMnemonic("ldarg")]
    @ldarg = 0x09,
    [CilMnemonic("ldarga")]
    @ldarga = 0x0A,
    [CilMnemonic("starg")]
    @starg = 0x0B,
    [CilMnemonic("ldloc")]
    @ldloc = 0x0C,
    [CilMnemonic("ldloca")]
    @ldloca = 0x0D,
    [CilMnemonic("stloc")]
    @stloc = 0x0E,
    [CilMnemonic("localloc")]
    @localloc = 0x0F,
    [CilMnemonic("endfilter")]
    @endfilter = 0x11,
    [CilMnemonic("unaligned.")]
    @unaligned = 0x12,
    [CilMnemonic("volatile.")]
    @volatile = 0x13,
    [CilMnemonic("tail.")]
    @tail = 0x14,
    [CilMnemonic("Initobj")]
    @Initobj = 0x15,
    [CilMnemonic("constrained.")]
    @constrained = 0x16,
    [CilMnemonic("cpblk")]
    @cpblk = 0x17,
    [CilMnemonic("initblk")]
    @initblk = 0x18,
    [CilMnemonic("no.")]
    @no = 0x19,
    [CilMnemonic("rethrow")]
    @rethrow = 0x1A,
    [CilMnemonic("sizeof")]
    @sizeof = 0x1C,
    [CilMnemonic("Refanytype")]
    @Refanytype = 0x1D,
    [CilMnemonic("readonly.")]
    @readonly = 0x1E,
}

