using System.Reflection;

namespace Reemit.Disassembler.Clr.Disassembler;

public static class OperandSizeTable
{
    // Todo:
    // Get rid of duplicate enum field code
    public static IReadOnlyDictionary<OperandType, int> SizeTable { get; } =
        typeof(OperandType)
            .GetFields()
            .Where(x => !x.IsSpecialName)
            .ToDictionary(
                x => (OperandType)x.GetRawConstantValue()!,
                x => x.GetCustomAttribute<OperandSizeAttribute>()!.SizeInBytes);
}
