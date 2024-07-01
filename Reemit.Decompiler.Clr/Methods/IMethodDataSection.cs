namespace Reemit.Decompiler.Clr.Methods;

public interface IMethodDataSection
{
    CorILMethodSectionFlags Kind { get; }
}