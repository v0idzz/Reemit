namespace Reemit.Disassembler.Clr.Methods;

public interface IMethodDataSection
{
    CorILMethodSectionFlags Kind { get; }
}