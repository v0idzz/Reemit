namespace Reemit.Decompiler.Clr.Disassembler;

[AttributeUsage(AttributeTargets.Field)]
public class CilMnemonicAttribute(string mnemonic) : Attribute
{
    public string Mnemonic { get; } = mnemonic;
}
