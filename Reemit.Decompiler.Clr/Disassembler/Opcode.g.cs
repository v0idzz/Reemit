namespace Reemit.Decompiler.Clr.Disassembler;

public enum Opcode : byte
{
    [CilMnemonic("nop")]
    @nop = 0x00,
    [CilMnemonic("break")]
    @break = 0x01,
    [CilMnemonic("ldarg.0")]
    @ldarg_0 = 0x02,
    [CilMnemonic("ldarg.1")]
    @ldarg_1 = 0x03,
    [CilMnemonic("ldarg.2")]
    @ldarg_2 = 0x04,
    [CilMnemonic("ldarg.3")]
    @ldarg_3 = 0x05,
    [CilMnemonic("ldloc.0")]
    @ldloc_0 = 0x06,
    [CilMnemonic("ldloc.1")]
    @ldloc_1 = 0x07,
    [CilMnemonic("ldloc.2")]
    @ldloc_2 = 0x08,
    [CilMnemonic("ldloc.3")]
    @ldloc_3 = 0x09,
    [CilMnemonic("stloc.0")]
    @stloc_0 = 0x0A,
    [CilMnemonic("stloc.1")]
    @stloc_1 = 0x0B,
    [CilMnemonic("stloc.2")]
    @stloc_2 = 0x0C,
    [CilMnemonic("stloc.3")]
    @stloc_3 = 0x0D,
    [CilMnemonic("ldarg.s")]
    @ldarg_s = 0x0E,
    [CilMnemonic("ldarga.s")]
    @ldarga_s = 0x0F,
    [CilMnemonic("starg.s")]
    @starg_s = 0x10,
    [CilMnemonic("ldloc.s")]
    @ldloc_s = 0x11,
    [CilMnemonic("ldloca.s")]
    @ldloca_s = 0x12,
    [CilMnemonic("stloc.s")]
    @stloc_s = 0x13,
    [CilMnemonic("ldnull")]
    @ldnull = 0x14,
    [CilMnemonic("ldc.i4.m1")]
    @ldc_i4_m1 = 0x15,
    [CilMnemonic("ldc.i4.0")]
    @ldc_i4_0 = 0x16,
    [CilMnemonic("ldc.i4.1")]
    @ldc_i4_1 = 0x17,
    [CilMnemonic("ldc.i4.2")]
    @ldc_i4_2 = 0x18,
    [CilMnemonic("ldc.i4.3")]
    @ldc_i4_3 = 0x19,
    [CilMnemonic("ldc.i4.4")]
    @ldc_i4_4 = 0x1A,
    [CilMnemonic("ldc.i4.5")]
    @ldc_i4_5 = 0x1B,
    [CilMnemonic("ldc.i4.6")]
    @ldc_i4_6 = 0x1C,
    [CilMnemonic("ldc.i4.7")]
    @ldc_i4_7 = 0x1D,
    [CilMnemonic("ldc.i4.8")]
    @ldc_i4_8 = 0x1E,
    [CilMnemonic("ldc.i4.s")]
    @ldc_i4_s = 0x1F,
    [CilMnemonic("ldc.i4")]
    @ldc_i4 = 0x20,
    [CilMnemonic("ldc.i8")]
    @ldc_i8 = 0x21,
    [CilMnemonic("ldc.r4")]
    @ldc_r4 = 0x22,
    [CilMnemonic("ldc.r8")]
    @ldc_r8 = 0x23,
    [CilMnemonic("dup")]
    @dup = 0x25,
    [CilMnemonic("pop")]
    @pop = 0x26,
    [CilMnemonic("jmp")]
    @jmp = 0x27,
    [CilMnemonic("call")]
    @call = 0x28,
    [CilMnemonic("calli")]
    @calli = 0x29,
    [CilMnemonic("ret")]
    @ret = 0x2A,
    [CilMnemonic("br.s")]
    @br_s = 0x2B,
    [CilMnemonic("brfalse.s")]
    @brfalse_s = 0x2C,
    [CilMnemonic("brtrue.s")]
    @brtrue_s = 0x2D,
    [CilMnemonic("beq.s")]
    @beq_s = 0x2E,
    [CilMnemonic("bge.s")]
    @bge_s = 0x2F,
    [CilMnemonic("bgt.s")]
    @bgt_s = 0x30,
    [CilMnemonic("ble.s")]
    @ble_s = 0x31,
    [CilMnemonic("blt.s")]
    @blt_s = 0x32,
    [CilMnemonic("bne.un.s")]
    @bne_un_s = 0x33,
    [CilMnemonic("bge.un.s")]
    @bge_un_s = 0x34,
    [CilMnemonic("bgt.un.s")]
    @bgt_un_s = 0x35,
    [CilMnemonic("ble.un.s")]
    @ble_un_s = 0x36,
    [CilMnemonic("blt.un.s")]
    @blt_un_s = 0x37,
    [CilMnemonic("br")]
    @br = 0x38,
    [CilMnemonic("brfalse")]
    @brfalse = 0x39,
    [CilMnemonic("brtrue")]
    @brtrue = 0x3A,
    [CilMnemonic("beq")]
    @beq = 0x3B,
    [CilMnemonic("bge")]
    @bge = 0x3C,
    [CilMnemonic("bgt")]
    @bgt = 0x3D,
    [CilMnemonic("ble")]
    @ble = 0x3E,
    [CilMnemonic("blt")]
    @blt = 0x3F,
    [CilMnemonic("bne.un")]
    @bne_un = 0x40,
    [CilMnemonic("bge.un")]
    @bge_un = 0x41,
    [CilMnemonic("bgt.un")]
    @bgt_un = 0x42,
    [CilMnemonic("ble.un")]
    @ble_un = 0x43,
    [CilMnemonic("blt.un")]
    @blt_un = 0x44,
    [CilMnemonic("switch")]
    @switch = 0x45,
    [CilMnemonic("ldind.i1")]
    @ldind_i1 = 0x46,
    [CilMnemonic("ldind.u1")]
    @ldind_u1 = 0x47,
    [CilMnemonic("ldind.i2")]
    @ldind_i2 = 0x48,
    [CilMnemonic("ldind.u2")]
    @ldind_u2 = 0x49,
    [CilMnemonic("ldind.i4")]
    @ldind_i4 = 0x4A,
    [CilMnemonic("ldind.u4")]
    @ldind_u4 = 0x4B,
    [CilMnemonic("ldind.i8")]
    @ldind_i8 = 0x4C,
    [CilMnemonic("ldind.i")]
    @ldind_i = 0x4D,
    [CilMnemonic("ldind.r4")]
    @ldind_r4 = 0x4E,
    [CilMnemonic("ldind.r8")]
    @ldind_r8 = 0x4F,
    [CilMnemonic("ldind.ref")]
    @ldind_ref = 0x50,
    [CilMnemonic("stind.ref")]
    @stind_ref = 0x51,
    [CilMnemonic("stind.i1")]
    @stind_i1 = 0x52,
    [CilMnemonic("stind.i2")]
    @stind_i2 = 0x53,
    [CilMnemonic("stind.i4")]
    @stind_i4 = 0x54,
    [CilMnemonic("stind.i8")]
    @stind_i8 = 0x55,
    [CilMnemonic("stind.r4")]
    @stind_r4 = 0x56,
    [CilMnemonic("stind.r8")]
    @stind_r8 = 0x57,
    [CilMnemonic("add")]
    @add = 0x58,
    [CilMnemonic("sub")]
    @sub = 0x59,
    [CilMnemonic("mul")]
    @mul = 0x5A,
    [CilMnemonic("div")]
    @div = 0x5B,
    [CilMnemonic("div.un")]
    @div_un = 0x5C,
    [CilMnemonic("rem")]
    @rem = 0x5D,
    [CilMnemonic("rem.un")]
    @rem_un = 0x5E,
    [CilMnemonic("and")]
    @and = 0x5F,
    [CilMnemonic("or")]
    @or = 0x60,
    [CilMnemonic("xor")]
    @xor = 0x61,
    [CilMnemonic("shl")]
    @shl = 0x62,
    [CilMnemonic("shr")]
    @shr = 0x63,
    [CilMnemonic("shr.un")]
    @shr_un = 0x64,
    [CilMnemonic("neg")]
    @neg = 0x65,
    [CilMnemonic("not")]
    @not = 0x66,
    [CilMnemonic("conv.i1")]
    @conv_i1 = 0x67,
    [CilMnemonic("conv.i2")]
    @conv_i2 = 0x68,
    [CilMnemonic("conv.i4")]
    @conv_i4 = 0x69,
    [CilMnemonic("conv.i8")]
    @conv_i8 = 0x6A,
    [CilMnemonic("conv.r4")]
    @conv_r4 = 0x6B,
    [CilMnemonic("conv.r8")]
    @conv_r8 = 0x6C,
    [CilMnemonic("conv.u4")]
    @conv_u4 = 0x6D,
    [CilMnemonic("conv.u8")]
    @conv_u8 = 0x6E,
    [CilMnemonic("callvirt")]
    @callvirt = 0x6F,
    [CilMnemonic("cpobj")]
    @cpobj = 0x70,
    [CilMnemonic("ldobj")]
    @ldobj = 0x71,
    [CilMnemonic("ldstr")]
    @ldstr = 0x72,
    [CilMnemonic("newobj")]
    @newobj = 0x73,
    [CilMnemonic("castclass")]
    @castclass = 0x74,
    [CilMnemonic("isinst")]
    @isinst = 0x75,
    [CilMnemonic("conv.r.un")]
    @conv_r_un = 0x76,
    [CilMnemonic("unbox")]
    @unbox = 0x79,
    [CilMnemonic("throw")]
    @throw = 0x7A,
    [CilMnemonic("ldfld")]
    @ldfld = 0x7B,
    [CilMnemonic("ldflda")]
    @ldflda = 0x7C,
    [CilMnemonic("stfld")]
    @stfld = 0x7D,
    [CilMnemonic("ldsfld")]
    @ldsfld = 0x7E,
    [CilMnemonic("ldsflda")]
    @ldsflda = 0x7F,
    [CilMnemonic("stsfld")]
    @stsfld = 0x80,
    [CilMnemonic("stobj")]
    @stobj = 0x81,
    [CilMnemonic("conv.ovf.i1.un")]
    @conv_ovf_i1_un = 0x82,
    [CilMnemonic("conv.ovf.i2.un")]
    @conv_ovf_i2_un = 0x83,
    [CilMnemonic("conv.ovf.i4.un")]
    @conv_ovf_i4_un = 0x84,
    [CilMnemonic("conv.ovf.i8.un")]
    @conv_ovf_i8_un = 0x85,
    [CilMnemonic("conv.ovf.u1.un")]
    @conv_ovf_u1_un = 0x86,
    [CilMnemonic("conv.ovf.u2.un")]
    @conv_ovf_u2_un = 0x87,
    [CilMnemonic("conv.ovf.u4.un")]
    @conv_ovf_u4_un = 0x88,
    [CilMnemonic("conv.ovf.u8.un")]
    @conv_ovf_u8_un = 0x89,
    [CilMnemonic("conv.ovf.i.un")]
    @conv_ovf_i_un = 0x8A,
    [CilMnemonic("conv.ovf.u.un")]
    @conv_ovf_u_un = 0x8B,
    [CilMnemonic("box")]
    @box = 0x8C,
    [CilMnemonic("newarr")]
    @newarr = 0x8D,
    [CilMnemonic("ldlen")]
    @ldlen = 0x8E,
    [CilMnemonic("ldelema")]
    @ldelema = 0x8F,
    [CilMnemonic("ldelem.i1")]
    @ldelem_i1 = 0x90,
    [CilMnemonic("ldelem.u1")]
    @ldelem_u1 = 0x91,
    [CilMnemonic("ldelem.i2")]
    @ldelem_i2 = 0x92,
    [CilMnemonic("ldelem.u2")]
    @ldelem_u2 = 0x93,
    [CilMnemonic("ldelem.i4")]
    @ldelem_i4 = 0x94,
    [CilMnemonic("ldelem.u4")]
    @ldelem_u4 = 0x95,
    [CilMnemonic("ldelem.i8")]
    @ldelem_i8 = 0x96,
    [CilMnemonic("ldelem.i")]
    @ldelem_i = 0x97,
    [CilMnemonic("ldelem.r4")]
    @ldelem_r4 = 0x98,
    [CilMnemonic("ldelem.r8")]
    @ldelem_r8 = 0x99,
    [CilMnemonic("ldelem.ref")]
    @ldelem_ref = 0x9A,
    [CilMnemonic("stelem.i")]
    @stelem_i = 0x9B,
    [CilMnemonic("stelem.i1")]
    @stelem_i1 = 0x9C,
    [CilMnemonic("stelem.i2")]
    @stelem_i2 = 0x9D,
    [CilMnemonic("stelem.i4")]
    @stelem_i4 = 0x9E,
    [CilMnemonic("stelem.i8")]
    @stelem_i8 = 0x9F,
    [CilMnemonic("stelem.r4")]
    @stelem_r4 = 0xA0,
    [CilMnemonic("stelem.r8")]
    @stelem_r8 = 0xA1,
    [CilMnemonic("stelem.ref")]
    @stelem_ref = 0xA2,
    [CilMnemonic("ldelem")]
    @ldelem = 0xA3,
    [CilMnemonic("stelem")]
    @stelem = 0xA4,
    [CilMnemonic("unbox.any")]
    @unbox_any = 0xA5,
    [CilMnemonic("conv.ovf.i1")]
    @conv_ovf_i1 = 0xB3,
    [CilMnemonic("conv.ovf.u1")]
    @conv_ovf_u1 = 0xB4,
    [CilMnemonic("conv.ovf.i2")]
    @conv_ovf_i2 = 0xB5,
    [CilMnemonic("conv.ovf.u2")]
    @conv_ovf_u2 = 0xB6,
    [CilMnemonic("conv.ovf.i4")]
    @conv_ovf_i4 = 0xB7,
    [CilMnemonic("conv.ovf.u4")]
    @conv_ovf_u4 = 0xB8,
    [CilMnemonic("conv.ovf.i8")]
    @conv_ovf_i8 = 0xB9,
    [CilMnemonic("conv.ovf.u8")]
    @conv_ovf_u8 = 0xBA,
    [CilMnemonic("refanyval")]
    @refanyval = 0xC2,
    [CilMnemonic("ckfinite")]
    @ckfinite = 0xC3,
    [CilMnemonic("mkrefany")]
    @mkrefany = 0xC6,
    [CilMnemonic("ldtoken")]
    @ldtoken = 0xD0,
    [CilMnemonic("conv.u2")]
    @conv_u2 = 0xD1,
    [CilMnemonic("conv.u1")]
    @conv_u1 = 0xD2,
    [CilMnemonic("conv.i")]
    @conv_i = 0xD3,
    [CilMnemonic("conv.ovf.i")]
    @conv_ovf_i = 0xD4,
    [CilMnemonic("conv.ovf.u")]
    @conv_ovf_u = 0xD5,
    [CilMnemonic("add.ovf")]
    @add_ovf = 0xD6,
    [CilMnemonic("add.ovf.un")]
    @add_ovf_un = 0xD7,
    [CilMnemonic("mul.ovf")]
    @mul_ovf = 0xD8,
    [CilMnemonic("mul.ovf.un")]
    @mul_ovf_un = 0xD9,
    [CilMnemonic("sub.ovf")]
    @sub_ovf = 0xDA,
    [CilMnemonic("sub.ovf.un")]
    @sub_ovf_un = 0xDB,
    [CilMnemonic("endfinally")]
    @endfinally = 0xDC,
    [CilMnemonic("leave")]
    @leave = 0xDD,
    [CilMnemonic("leave.s")]
    @leave_s = 0xDE,
    [CilMnemonic("stind.i")]
    @stind_i = 0xDF,
    [CilMnemonic("conv.u")]
    @conv_u = 0xE0,
    Extended = 0xFE,
}

