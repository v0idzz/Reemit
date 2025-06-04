using System.Reflection;

namespace Reemit.Disassembler.Clr.Disassembler;

public static class MnemonicTable
{
    public static IReadOnlyDictionary<Opcode, string> Standard { get; } = CreateTable<Opcode>();

    public static IReadOnlyDictionary<ExtendedOpcode, string> Extended { get; } = CreateTable<ExtendedOpcode>();

    public static IReadOnlyDictionary<TOpcode, string> CreateTable<TOpcode>()
        where TOpcode : Enum =>
        typeof(TOpcode)
            .GetFields()
            .Where(x => !x.IsSpecialName)
            .Select(x => new
            {
                Opcode = (TOpcode)x.GetRawConstantValue()!,
                MnemonicAttr = x.GetCustomAttribute<MnemonicAttribute>(),
            })
            .Where(x => x.MnemonicAttr != null)
            .ToDictionary(
                x => x.Opcode,
                x => x.MnemonicAttr!.Mnemonic);

    public static string GetMnemonic(OpcodeInfo opcodeInfo) =>
        opcodeInfo.IsExtended ?
            Extended[opcodeInfo.ExtendedOpcode] :
            Standard[opcodeInfo.Opcode];
}