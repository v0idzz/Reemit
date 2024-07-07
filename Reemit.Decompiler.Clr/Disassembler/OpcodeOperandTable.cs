using System.Collections.ObjectModel;

namespace Reemit.Decompiler.Clr.Disassembler;

public static class OpcodeOperandTable
{
    public static ReadOnlyDictionary<Opcode, OperandType> Standard { get; } =
        new ReadOnlyDictionary<Opcode, OperandType>(
            new Dictionary<Opcode, OperandType>()
            {
                // Partition III 3.5
                { Opcode.beq, OperandType.Int32 },
                { Opcode.beq_s, OperandType.Int8 },

                // Partition III 3.6
                { Opcode.bge, OperandType.Int32 },
                { Opcode.bge_s, OperandType.Int8 },

                // Partition III 3.7
                { Opcode.bge_un, OperandType.Int32 },
                { Opcode.bge_un_s, OperandType.Int8 },

                // Partition III 3.8
                { Opcode.bgt, OperandType.Int32 },
                { Opcode.bgt_s, OperandType.Int8 },

                // Partition III 3.9
                { Opcode.bgt_un, OperandType.Int32 },
                { Opcode.bgt_un_s, OperandType.Int8 },

                // Partition III 3.10
                { Opcode.ble, OperandType.Int32 },
                { Opcode.ble_s, OperandType.Int8 },

                // Partition III 3.11
                { Opcode.ble_un, OperandType.Int32 },
                { Opcode.ble_un_s, OperandType.Int8 },

                // Partition III 3.12
                { Opcode.blt, OperandType.Int32 },
                { Opcode.blt_s, OperandType.Int8 },

                // Partition III 3.13
                { Opcode.blt_un, OperandType.Int32 },
                { Opcode.blt_un_s, OperandType.Int8 },

                // Partition III 3.14
                { Opcode.bne_un, OperandType.Int32 },
                { Opcode.bne_un_s, OperandType.Int8 },

                // Partition III 3.15
                { Opcode.br, OperandType.Int32 },
                { Opcode.br_s, OperandType.Int8 },

                // Partition III 3.17
                { Opcode.brfalse, OperandType.Int32 },
                { Opcode.brfalse_s, OperandType.Int8 },

                // Partition III 3.18
                { Opcode.brtrue, OperandType.Int32 },
                { Opcode.brtrue_s, OperandType.Int8 },

                // Partition III 3.19
                { Opcode.call, OperandType.MetadataToken },

                // Partition III 3.20
                { Opcode.calli, OperandType.MetadataToken },

                // Partition III 3.37
                { Opcode.jmp, OperandType.MetadataToken },

                // Partition III 3.38
                // Todo: extended
                { Opcode.ldarg_s, OperandType.UInt8 },

                // Partition III 3.39
                // Todo: extended
                { Opcode.ldarga_s, OperandType.UInt8 },

                // Partition III 3.40
                { Opcode.ldc_i4, OperandType.Int32 },
                { Opcode.ldc_i8, OperandType.Int64 },
                { Opcode.ldc_r4, OperandType.Float32 },
                { Opcode.ldc_r8, OperandType.Float64 },
                { Opcode.ldc_i4_s, OperandType.Int8 },

                // Partition III 3.43
                // Todo: extended
                { Opcode.ldloc_s, OperandType.UInt8 },

                // Partition III 3.44
                // Todo: extended
                { Opcode.ldloca_s, OperandType.UInt8 },

                // Partition III 3.46
                { Opcode.leave, OperandType.Int32 },
                { Opcode.leave_s, OperandType.Int8 },

                // Partition III 3.61
                // Todo: extended
                { Opcode.starg_s, OperandType.UInt8 },

                // Partition III 3.63
                // Todo: extended
                { Opcode.stloc_s, OperandType.UInt8 },

                // Partition III 3.63
                { Opcode.@switch, OperandType.JumpTable },

                // Partition III 4.1
                { Opcode.box, OperandType.MetadataToken },

                // Partition III 4.2
                { Opcode.callvirt, OperandType.MetadataToken },

                // Partition III 4.3
                { Opcode.castclass, OperandType.MetadataToken },

                // Partition III 4.4
                { Opcode.cpobj, OperandType.MetadataToken },

                // Partition III 4.6
                { Opcode.isinst, OperandType.MetadataToken },

                // Partition III 4.7
                { Opcode.ldelem, OperandType.MetadataToken },

                // Partition III 4.9
                { Opcode.ldelema, OperandType.MetadataToken },

                // Partition III 4.10
                { Opcode.ldfld, OperandType.MetadataToken },

                // Partition III 4.11
                { Opcode.ldflda, OperandType.MetadataToken },

                // Partition III 4.13
                { Opcode.ldobj, OperandType.MetadataToken },

                // Partition III 4.14
                { Opcode.ldsfld, OperandType.MetadataToken },

                // Partition III 4.15
                { Opcode.ldsflda, OperandType.MetadataToken },

                // Partition III 4.16
                { Opcode.ldstr, OperandType.MetadataToken },

                // Partition III 4.17
                { Opcode.ldtoken, OperandType.MetadataToken },

                // Partition III 4.19
                { Opcode.mkrefany, OperandType.MetadataToken },

                // Partition III 4.20
                { Opcode.newarr, OperandType.MetadataToken },

                // Partition III 4.21
                { Opcode.newobj, OperandType.MetadataToken },

                // Partition III 4.23
                { Opcode.refanyval, OperandType.MetadataToken },

                // Partition III 4.26
                { Opcode.stelem, OperandType.MetadataToken },

                // Partition III 4.28
                { Opcode.stfld, OperandType.MetadataToken },

                // Partition III 4.29
                { Opcode.stobj, OperandType.MetadataToken },

                // Partition III 4.30
                { Opcode.stsfld, OperandType.MetadataToken },

                // Partition III 4.32
                { Opcode.unbox, OperandType.MetadataToken },

                // Partition III 4.33
                { Opcode.unbox_any, OperandType.MetadataToken },
            });

    public static ReadOnlyDictionary<ExtendedOpcode, OperandType> Extended { get; } =
        new ReadOnlyDictionary<ExtendedOpcode, OperandType>(
            new Dictionary<ExtendedOpcode, OperandType>()
            {
                // Partition III 2.1
                { ExtendedOpcode.constrained, OperandType.MetadataToken },

                // Partition III 2.2
                { ExtendedOpcode.no, OperandType.UInt8 },

                // Partition III 2.5
                { ExtendedOpcode.unaligned, OperandType.UInt8 },

                // Partition III 3.38
                { ExtendedOpcode.ldarg, OperandType.UInt16 },

                // Partition III 3.39
                { ExtendedOpcode.ldarga, OperandType.UInt16 },

                // Partition III 3.41
                { ExtendedOpcode.ldftn, OperandType.MetadataToken },

                // Partition III 3.43
                { ExtendedOpcode.ldloc, OperandType.UInt16 },

                // Partition III 3.44
                { ExtendedOpcode.ldloca, OperandType.UInt16 },

                // Partition III 3.61
                { ExtendedOpcode.starg, OperandType.UInt16 },

                // Partition III 3.63
                { ExtendedOpcode.stloc, OperandType.UInt16 },

                // Partition III 4.5
                { ExtendedOpcode.Initobj, OperandType.MetadataToken },

                // Partition III 4.18
                { ExtendedOpcode.ldvirtftn, OperandType.MetadataToken },

                // Partition III 4.25
                { ExtendedOpcode.@sizeof, OperandType.MetadataToken },
            });

    public static OperandType GetOperandType(OpcodeInfo opcodeInfo) =>
        opcodeInfo.IsExtended ?
            GetOperandType(opcodeInfo.ExtendedOpcode) :
            GetOperandType(opcodeInfo.Opcode);

    public static OperandType GetOperandType(Opcode opcode) =>
        GetOperandType(Standard, opcode);

    public static OperandType GetOperandType(ExtendedOpcode extendedOpcode) =>
        GetOperandType(Extended, extendedOpcode);

    private static OperandType GetOperandType<TOpcode>(
        IReadOnlyDictionary<TOpcode, OperandType> table,
        TOpcode opcode) =>
        // Assuming no entry means no operand for now. However, this behavior
        // may need to be changed to ensure we reject invalid opcodes.
        table.TryGetValue(opcode, out var value) ?
            value :
            OperandType.None;
}
