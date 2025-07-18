using Reemit.Disassembler.Clr.Disassembler;

namespace Reemit.Disassembler.Clr.Disassembler;

public enum Opcode : byte
{
    [Mnemonic("nop", false)]
    @nop = 0x00,

    [Mnemonic("break", false)]
    @break = 0x01,

    [Mnemonic("ldarg.0", false)]
    @ldarg_0 = 0x02,

    [Mnemonic("ldarg.1", false)]
    @ldarg_1 = 0x03,

    [Mnemonic("ldarg.2", false)]
    @ldarg_2 = 0x04,

    [Mnemonic("ldarg.3", false)]
    @ldarg_3 = 0x05,

    [Mnemonic("ldloc.0", false)]
    @ldloc_0 = 0x06,

    [Mnemonic("ldloc.1", false)]
    @ldloc_1 = 0x07,

    [Mnemonic("ldloc.2", false)]
    @ldloc_2 = 0x08,

    [Mnemonic("ldloc.3", false)]
    @ldloc_3 = 0x09,

    [Mnemonic("stloc.0", false)]
    @stloc_0 = 0x0A,

    [Mnemonic("stloc.1", false)]
    @stloc_1 = 0x0B,

    [Mnemonic("stloc.2", false)]
    @stloc_2 = 0x0C,

    [Mnemonic("stloc.3", false)]
    @stloc_3 = 0x0D,

    [Mnemonic("ldarg.s", false)]
    @ldarg_s = 0x0E,

    [Mnemonic("ldarga.s", false)]
    @ldarga_s = 0x0F,

    [Mnemonic("starg.s", false)]
    @starg_s = 0x10,

    [Mnemonic("ldloc.s", false)]
    @ldloc_s = 0x11,

    [Mnemonic("ldloca.s", false)]
    @ldloca_s = 0x12,

    [Mnemonic("stloc.s", false)]
    @stloc_s = 0x13,

    [Mnemonic("ldnull", false)]
    @ldnull = 0x14,

    [Mnemonic("ldc.i4.m1", false)]
    @ldc_i4_m1 = 0x15,

    [Mnemonic("ldc.i4.0", false)]
    @ldc_i4_0 = 0x16,

    [Mnemonic("ldc.i4.1", false)]
    @ldc_i4_1 = 0x17,

    [Mnemonic("ldc.i4.2", false)]
    @ldc_i4_2 = 0x18,

    [Mnemonic("ldc.i4.3", false)]
    @ldc_i4_3 = 0x19,

    [Mnemonic("ldc.i4.4", false)]
    @ldc_i4_4 = 0x1A,

    [Mnemonic("ldc.i4.5", false)]
    @ldc_i4_5 = 0x1B,

    [Mnemonic("ldc.i4.6", false)]
    @ldc_i4_6 = 0x1C,

    [Mnemonic("ldc.i4.7", false)]
    @ldc_i4_7 = 0x1D,

    [Mnemonic("ldc.i4.8", false)]
    @ldc_i4_8 = 0x1E,

    [Mnemonic("ldc.i4.s", false)]
    @ldc_i4_s = 0x1F,

    [Mnemonic("ldc.i4", false)]
    @ldc_i4 = 0x20,

    [Mnemonic("ldc.i8", false)]
    @ldc_i8 = 0x21,

    [Mnemonic("ldc.r4", false)]
    @ldc_r4 = 0x22,

    [Mnemonic("ldc.r8", false)]
    @ldc_r8 = 0x23,

    [Mnemonic("dup", false)]
    @dup = 0x25,

    [Mnemonic("pop", false)]
    @pop = 0x26,

    [Mnemonic("jmp", false)]
    @jmp = 0x27,

    [Mnemonic("call", false)]
    @call = 0x28,

    [Mnemonic("calli", false)]
    @calli = 0x29,

    [Mnemonic("ret", false)]
    @ret = 0x2A,

    [Mnemonic("br.s", false)]
    @br_s = 0x2B,

    [Mnemonic("brfalse.s", false)]
    @brfalse_s = 0x2C,

    [Mnemonic("brtrue.s", false)]
    @brtrue_s = 0x2D,

    [Mnemonic("beq.s", false)]
    @beq_s = 0x2E,

    [Mnemonic("bge.s", false)]
    @bge_s = 0x2F,

    [Mnemonic("bgt.s", false)]
    @bgt_s = 0x30,

    [Mnemonic("ble.s", false)]
    @ble_s = 0x31,

    [Mnemonic("blt.s", false)]
    @blt_s = 0x32,

    [Mnemonic("bne.un.s", false)]
    @bne_un_s = 0x33,

    [Mnemonic("bge.un.s", false)]
    @bge_un_s = 0x34,

    [Mnemonic("bgt.un.s", false)]
    @bgt_un_s = 0x35,

    [Mnemonic("ble.un.s", false)]
    @ble_un_s = 0x36,

    [Mnemonic("blt.un.s", false)]
    @blt_un_s = 0x37,

    [Mnemonic("br", false)]
    @br = 0x38,

    [Mnemonic("brfalse", false)]
    @brfalse = 0x39,

    [Mnemonic("brtrue", false)]
    @brtrue = 0x3A,

    [Mnemonic("beq", false)]
    @beq = 0x3B,

    [Mnemonic("bge", false)]
    @bge = 0x3C,

    [Mnemonic("bgt", false)]
    @bgt = 0x3D,

    [Mnemonic("ble", false)]
    @ble = 0x3E,

    [Mnemonic("blt", false)]
    @blt = 0x3F,

    [Mnemonic("bne.un", false)]
    @bne_un = 0x40,

    [Mnemonic("bge.un", false)]
    @bge_un = 0x41,

    [Mnemonic("bgt.un", false)]
    @bgt_un = 0x42,

    [Mnemonic("ble.un", false)]
    @ble_un = 0x43,

    [Mnemonic("blt.un", false)]
    @blt_un = 0x44,

    [Mnemonic("switch", false)]
    @switch = 0x45,

    [Mnemonic("ldind.i1", false)]
    @ldind_i1 = 0x46,

    [Mnemonic("ldind.u1", false)]
    @ldind_u1 = 0x47,

    [Mnemonic("ldind.i2", false)]
    @ldind_i2 = 0x48,

    [Mnemonic("ldind.u2", false)]
    @ldind_u2 = 0x49,

    [Mnemonic("ldind.i4", false)]
    @ldind_i4 = 0x4A,

    [Mnemonic("ldind.u4", false)]
    @ldind_u4 = 0x4B,

    [Mnemonic("ldind.i8", false)]
    @ldind_i8 = 0x4C,

    [Mnemonic("ldind.i", false)]
    @ldind_i = 0x4D,

    [Mnemonic("ldind.r4", false)]
    @ldind_r4 = 0x4E,

    [Mnemonic("ldind.r8", false)]
    @ldind_r8 = 0x4F,

    [Mnemonic("ldind.ref", false)]
    @ldind_ref = 0x50,

    [Mnemonic("stind.ref", false)]
    @stind_ref = 0x51,

    [Mnemonic("stind.i1", false)]
    @stind_i1 = 0x52,

    [Mnemonic("stind.i2", false)]
    @stind_i2 = 0x53,

    [Mnemonic("stind.i4", false)]
    @stind_i4 = 0x54,

    [Mnemonic("stind.i8", false)]
    @stind_i8 = 0x55,

    [Mnemonic("stind.r4", false)]
    @stind_r4 = 0x56,

    [Mnemonic("stind.r8", false)]
    @stind_r8 = 0x57,

    [Mnemonic("add", false)]
    @add = 0x58,

    [Mnemonic("sub", false)]
    @sub = 0x59,

    [Mnemonic("mul", false)]
    @mul = 0x5A,

    [Mnemonic("div", false)]
    @div = 0x5B,

    [Mnemonic("div.un", false)]
    @div_un = 0x5C,

    [Mnemonic("rem", false)]
    @rem = 0x5D,

    [Mnemonic("rem.un", false)]
    @rem_un = 0x5E,

    [Mnemonic("and", false)]
    @and = 0x5F,

    [Mnemonic("or", false)]
    @or = 0x60,

    [Mnemonic("xor", false)]
    @xor = 0x61,

    [Mnemonic("shl", false)]
    @shl = 0x62,

    [Mnemonic("shr", false)]
    @shr = 0x63,

    [Mnemonic("shr.un", false)]
    @shr_un = 0x64,

    [Mnemonic("neg", false)]
    @neg = 0x65,

    [Mnemonic("not", false)]
    @not = 0x66,

    [Mnemonic("conv.i1", false)]
    @conv_i1 = 0x67,

    [Mnemonic("conv.i2", false)]
    @conv_i2 = 0x68,

    [Mnemonic("conv.i4", false)]
    @conv_i4 = 0x69,

    [Mnemonic("conv.i8", false)]
    @conv_i8 = 0x6A,

    [Mnemonic("conv.r4", false)]
    @conv_r4 = 0x6B,

    [Mnemonic("conv.r8", false)]
    @conv_r8 = 0x6C,

    [Mnemonic("conv.u4", false)]
    @conv_u4 = 0x6D,

    [Mnemonic("conv.u8", false)]
    @conv_u8 = 0x6E,

    [Mnemonic("callvirt", false)]
    @callvirt = 0x6F,

    [Mnemonic("cpobj", false)]
    @cpobj = 0x70,

    [Mnemonic("ldobj", false)]
    @ldobj = 0x71,

    [Mnemonic("ldstr", false)]
    @ldstr = 0x72,

    [Mnemonic("newobj", false)]
    @newobj = 0x73,

    [Mnemonic("castclass", false)]
    @castclass = 0x74,

    [Mnemonic("isinst", false)]
    @isinst = 0x75,

    [Mnemonic("conv.r.un", false)]
    @conv_r_un = 0x76,

    [Mnemonic("unbox", false)]
    @unbox = 0x79,

    [Mnemonic("throw", false)]
    @throw = 0x7A,

    [Mnemonic("ldfld", false)]
    @ldfld = 0x7B,

    [Mnemonic("ldflda", false)]
    @ldflda = 0x7C,

    [Mnemonic("stfld", false)]
    @stfld = 0x7D,

    [Mnemonic("ldsfld", false)]
    @ldsfld = 0x7E,

    [Mnemonic("ldsflda", false)]
    @ldsflda = 0x7F,

    [Mnemonic("stsfld", false)]
    @stsfld = 0x80,

    [Mnemonic("stobj", false)]
    @stobj = 0x81,

    [Mnemonic("conv.ovf.i1.un", false)]
    @conv_ovf_i1_un = 0x82,

    [Mnemonic("conv.ovf.i2.un", false)]
    @conv_ovf_i2_un = 0x83,

    [Mnemonic("conv.ovf.i4.un", false)]
    @conv_ovf_i4_un = 0x84,

    [Mnemonic("conv.ovf.i8.un", false)]
    @conv_ovf_i8_un = 0x85,

    [Mnemonic("conv.ovf.u1.un", false)]
    @conv_ovf_u1_un = 0x86,

    [Mnemonic("conv.ovf.u2.un", false)]
    @conv_ovf_u2_un = 0x87,

    [Mnemonic("conv.ovf.u4.un", false)]
    @conv_ovf_u4_un = 0x88,

    [Mnemonic("conv.ovf.u8.un", false)]
    @conv_ovf_u8_un = 0x89,

    [Mnemonic("conv.ovf.i.un", false)]
    @conv_ovf_i_un = 0x8A,

    [Mnemonic("conv.ovf.u.un", false)]
    @conv_ovf_u_un = 0x8B,

    [Mnemonic("box", false)]
    @box = 0x8C,

    [Mnemonic("newarr", false)]
    @newarr = 0x8D,

    [Mnemonic("ldlen", false)]
    @ldlen = 0x8E,

    [Mnemonic("ldelema", false)]
    @ldelema = 0x8F,

    [Mnemonic("ldelem.i1", false)]
    @ldelem_i1 = 0x90,

    [Mnemonic("ldelem.u1", false)]
    @ldelem_u1 = 0x91,

    [Mnemonic("ldelem.i2", false)]
    @ldelem_i2 = 0x92,

    [Mnemonic("ldelem.u2", false)]
    @ldelem_u2 = 0x93,

    [Mnemonic("ldelem.i4", false)]
    @ldelem_i4 = 0x94,

    [Mnemonic("ldelem.u4", false)]
    @ldelem_u4 = 0x95,

    [Mnemonic("ldelem.i8", false)]
    @ldelem_i8 = 0x96,

    [Mnemonic("ldelem.i", false)]
    @ldelem_i = 0x97,

    [Mnemonic("ldelem.r4", false)]
    @ldelem_r4 = 0x98,

    [Mnemonic("ldelem.r8", false)]
    @ldelem_r8 = 0x99,

    [Mnemonic("ldelem.ref", false)]
    @ldelem_ref = 0x9A,

    [Mnemonic("stelem.i", false)]
    @stelem_i = 0x9B,

    [Mnemonic("stelem.i1", false)]
    @stelem_i1 = 0x9C,

    [Mnemonic("stelem.i2", false)]
    @stelem_i2 = 0x9D,

    [Mnemonic("stelem.i4", false)]
    @stelem_i4 = 0x9E,

    [Mnemonic("stelem.i8", false)]
    @stelem_i8 = 0x9F,

    [Mnemonic("stelem.r4", false)]
    @stelem_r4 = 0xA0,

    [Mnemonic("stelem.r8", false)]
    @stelem_r8 = 0xA1,

    [Mnemonic("stelem.ref", false)]
    @stelem_ref = 0xA2,

    [Mnemonic("ldelem", false)]
    @ldelem = 0xA3,

    [Mnemonic("stelem", false)]
    @stelem = 0xA4,

    [Mnemonic("unbox.any", false)]
    @unbox_any = 0xA5,

    [Mnemonic("conv.ovf.i1", false)]
    @conv_ovf_i1 = 0xB3,

    [Mnemonic("conv.ovf.u1", false)]
    @conv_ovf_u1 = 0xB4,

    [Mnemonic("conv.ovf.i2", false)]
    @conv_ovf_i2 = 0xB5,

    [Mnemonic("conv.ovf.u2", false)]
    @conv_ovf_u2 = 0xB6,

    [Mnemonic("conv.ovf.i4", false)]
    @conv_ovf_i4 = 0xB7,

    [Mnemonic("conv.ovf.u4", false)]
    @conv_ovf_u4 = 0xB8,

    [Mnemonic("conv.ovf.i8", false)]
    @conv_ovf_i8 = 0xB9,

    [Mnemonic("conv.ovf.u8", false)]
    @conv_ovf_u8 = 0xBA,

    [Mnemonic("refanyval", false)]
    @refanyval = 0xC2,

    [Mnemonic("ckfinite", false)]
    @ckfinite = 0xC3,

    [Mnemonic("mkrefany", false)]
    @mkrefany = 0xC6,

    [Mnemonic("ldtoken", false)]
    @ldtoken = 0xD0,

    [Mnemonic("conv.u2", false)]
    @conv_u2 = 0xD1,

    [Mnemonic("conv.u1", false)]
    @conv_u1 = 0xD2,

    [Mnemonic("conv.i", false)]
    @conv_i = 0xD3,

    [Mnemonic("conv.ovf.i", false)]
    @conv_ovf_i = 0xD4,

    [Mnemonic("conv.ovf.u", false)]
    @conv_ovf_u = 0xD5,

    [Mnemonic("add.ovf", false)]
    @add_ovf = 0xD6,

    [Mnemonic("add.ovf.un", false)]
    @add_ovf_un = 0xD7,

    [Mnemonic("mul.ovf", false)]
    @mul_ovf = 0xD8,

    [Mnemonic("mul.ovf.un", false)]
    @mul_ovf_un = 0xD9,

    [Mnemonic("sub.ovf", false)]
    @sub_ovf = 0xDA,

    [Mnemonic("sub.ovf.un", false)]
    @sub_ovf_un = 0xDB,

    [Mnemonic("endfinally", false)]
    @endfinally = 0xDC,

    [Mnemonic("leave", false)]
    @leave = 0xDD,

    [Mnemonic("leave.s", false)]
    @leave_s = 0xDE,

    [Mnemonic("stind.i", false)]
    @stind_i = 0xDF,

    [Mnemonic("conv.u", false)]
    @conv_u = 0xE0,

    Extended = 0xFE,
}

