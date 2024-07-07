using System.Collections.ObjectModel;

namespace Reemit.Decompiler.Clr.Disassembler;

public static class OperandSizeTable
{
    public static ReadOnlyDictionary<Opcode, OperandTypeFlags> Standard { get; } =
        new ReadOnlyDictionary<Opcode, OperandTypeFlags>(
            new Dictionary<Opcode, OperandTypeFlags>()
            {
                // Partition III 3.5
                { Opcode.beq, OperandTypeFlags.Int32 },
                { Opcode.beq_s, OperandTypeFlags.Int8 },

                // Partition III 3.6
                { Opcode.bge, OperandTypeFlags.Int32 },
                { Opcode.bge_s, OperandTypeFlags.Int8 },

                // Partition III 3.7
                { Opcode.bge_un, OperandTypeFlags.Int32 },
                { Opcode.bge_un_s, OperandTypeFlags.Int8 },

                // Partition III 3.8
                { Opcode.bgt, OperandTypeFlags.Int32 },
                { Opcode.bgt_s, OperandTypeFlags.Int8 },

                // Partition III 3.9
                { Opcode.bgt_un, OperandTypeFlags.Int32 },
                { Opcode.bgt_un_s, OperandTypeFlags.Int8 },

                // Partition III 3.10
                { Opcode.ble, OperandTypeFlags.Int32 },
                { Opcode.ble_s, OperandTypeFlags.Int8 },

                // Partition III 3.11
                { Opcode.ble_un, OperandTypeFlags.Int32 },
                { Opcode.ble_un_s, OperandTypeFlags.Int8 },

                // Partition III 3.12
                { Opcode.blt, OperandTypeFlags.Int32 },
                { Opcode.blt_s, OperandTypeFlags.Int8 },

                // Partition III 3.13
                { Opcode.blt_un, OperandTypeFlags.Int32 },
                { Opcode.blt_un_s, OperandTypeFlags.Int8 },

                // Partition III 3.14
                { Opcode.bne_un, OperandTypeFlags.Int32 },
                { Opcode.bne_un_s, OperandTypeFlags.Int8 },

                // Partition III 3.15
                { Opcode.br, OperandTypeFlags.Int32 },
                { Opcode.br_s, OperandTypeFlags.Int8 },

                // Partition III 3.17
                { Opcode.brfalse, OperandTypeFlags.Int32 },
                { Opcode.brfalse_s, OperandTypeFlags.Int8 },

                // Partition III 3.18
                { Opcode.brtrue, OperandTypeFlags.Int32 },
                { Opcode.brtrue_s, OperandTypeFlags.Int8 },

                // Partition III 3.19
                { Opcode.call, OperandTypeFlags.MetadataToken },

                // Partition III 3.20
                { Opcode.calli, OperandTypeFlags.MetadataToken },

                // Partition III 3.37
                { Opcode.jmp, OperandTypeFlags.MetadataToken },

                // Partition III 3.38
                // Todo: extended
                { Opcode.ldarg_s, OperandTypeFlags.UInt8 },

                // Partition III 3.39
                // Todo: extended
                { Opcode.ldarga_s, OperandTypeFlags.UInt8 },

                // Partition III 3.40
                { Opcode.ldc_i4, OperandTypeFlags.Int32 },
                { Opcode.ldc_i8, OperandTypeFlags.Int64 },
                { Opcode.ldc_r4, OperandTypeFlags.Float32 },
                { Opcode.ldc_r8, OperandTypeFlags.Float64 },
                { Opcode.ldc_i4_s, OperandTypeFlags.Int8 },

                // Partition III 3.43
                // Todo: extended
                { Opcode.ldloc_s, OperandTypeFlags.UInt8 },

                // Partition III 3.44
                // Todo: extended
                { Opcode.ldloca_s, OperandTypeFlags.UInt8 },

                // Partition III 3.46
                { Opcode.leave, OperandTypeFlags.Int32 },
                { Opcode.leave_s, OperandTypeFlags.Int8 },

                // Partition III 3.61
                // Todo: extended
                { Opcode.starg_s, OperandTypeFlags.UInt8 },

                // Partition III 3.63
                // Todo: extended
                { Opcode.stloc_s, OperandTypeFlags.UInt8 },

                // Partition III 3.63
                // Todo: extended
                { Opcode.@switch, OperandTypeFlags.JumpTable },
            });
}
