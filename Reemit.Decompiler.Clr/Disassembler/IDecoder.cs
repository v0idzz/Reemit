namespace Reemit.Decompiler.Clr.Disassembler;

public interface IDecoder<T>
{
    T Decode();
}
