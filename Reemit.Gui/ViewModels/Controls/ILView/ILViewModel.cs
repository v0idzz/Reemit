using System.IO;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reemit.Disassembler;
using Reemit.Disassembler.Clr.Disassembler;
using Reemit.Gui.ViewModels.Controls.ModuleExplorer;
using Reemit.Gui.ViewModels.Navigation;

namespace Reemit.Gui.ViewModels.Controls.ILView;

public class ILViewModel : ReactiveObject
{
    [ObservableAsProperty]
    public string? ILCode { get; set; }

    private readonly InstructionEmitter _emitter;

    public ILViewModel(InstructionEmitter emitter)
    {
        _emitter = emitter;

        NavigationMessageBus
            .ListenForNavigation()
            .Select(x => x.SelectedNode)
            .Select(node =>
            {
                if (node is ModuleExplorerMethodNodeViewModel methodNode)
                {
                    return ToILCode(methodNode.Method);
                }

                return null;
            })
            .ToPropertyEx(this, vm => vm.ILCode);
    }

    private string ToILCode(ClrMethod method)
    {
        using var ms = new MemoryStream(method.MethodBody);
        var disassembler = new StreamDisassembler(ms);

        var instructions = disassembler.Disassemble();
        return _emitter.Emit(instructions);
    }
}