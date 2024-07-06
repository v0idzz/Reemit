namespace Reemit.Decompiler.Clr.Disassembler;

[AttributeUsage(AttributeTargets.Field)]
public class MnemonicAttribute(string mnemonic, bool isExtended) : Attribute
{
    public string Mnemonic { get; } = mnemonic;

    public bool IsExtended { get; } = isExtended;
}
