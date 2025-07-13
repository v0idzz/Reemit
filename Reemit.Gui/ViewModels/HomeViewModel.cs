using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Dock.Model.Controls;
using DynamicData;
using ReactiveUI;
using Reemit.Disassembler;
using Reemit.Disassembler.Clr.Disassembler;
using Reemit.Gui.ViewModels.Controls.HexEditor;
using Reemit.Gui.ViewModels.Controls.ILView;
using Reemit.Gui.ViewModels.Controls.ModuleExplorer;
using Reemit.Gui.ViewModels.Dockables;
using Reemit.Gui.ViewModels.Navigation;

namespace Reemit.Gui.ViewModels;

public class HomeViewModel : ReactiveObject, IRoutableViewModel
{
    public ModuleExplorerTreeViewModel ModuleExplorerTreeViewModel { get; }
    public HexEditorViewModel HexEditorViewModel { get; }
    public ILViewModel ILViewModel { get; }
    public IRootDock? Layout { get; }

    public HomeViewModel(IScreen hostScreen, IReadOnlyList<ClrModule> modules)
    {
        var modulesSource = new SourceList<ClrModule>();
        modulesSource.AddRange(modules);
        modulesSource.Connect()
            .Bind(out var observableModules)
            .Subscribe();

        HostScreen = hostScreen;
        ModuleExplorerTreeViewModel = new ModuleExplorerTreeViewModel(observableModules);
        HexEditorViewModel = new HexEditorViewModel();
        ILViewModel = new ILViewModel(new InstructionEmitter());

        this.WhenAnyValue(x => x.ModuleExplorerTreeViewModel.SelectedNode)
            .Select(x => x?.Module.Bytes)
            .WhereNotNull()
            .BindTo(HexEditorViewModel, x => x.ModuleBytes);

        NavigationMessageBus.RegisterMessageSource(
            this.WhenAnyValue(x => x.ModuleExplorerTreeViewModel.SelectedNode)
                .Select(x => x as IRangeMappedNode)
                .WhereNotNull()
                .Select(x => new NavigationRequestMessage(x.Range, x)));

        var factory = new DockFactory(ModuleExplorerTreeViewModel, HexEditorViewModel, ILViewModel);
        var layout = factory.CreateLayout();
        factory.InitLayout(layout);
        Layout = layout;
    }

    public string UrlPathSegment => nameof(HomeViewModel);
    public IScreen HostScreen { get; }
}