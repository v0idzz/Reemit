using System;
using System.Collections.Generic;
using Dock.Avalonia.Controls;
using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.ReactiveUI;
using Dock.Model.ReactiveUI.Controls;
using Reemit.Gui.ViewModels.Controls.HexEditor;
using Reemit.Gui.ViewModels.Controls.ModuleExplorer;

namespace Reemit.Gui.ViewModels.Dockables;

public class DockFactory(ModuleExplorerTreeViewModel moduleExplorerViewModel, HexEditorViewModel hexEditorViewModel)
    : Factory
{
    public override IRootDock CreateLayout()
    {
        var hexEditor = new DockableDocumentViewModel(hexEditorViewModel)
        {
            CanClose = false,
            Title = "Hex"
        };

        var moduleExplorer = new DockableToolViewModel(moduleExplorerViewModel)
        {
            Title = "Module Explorer",
            CanClose = false,
        };

        var tools = new ProportionalDock
        {
            Proportion = 0.3,
            Orientation = Orientation.Vertical,
            VisibleDockables = CreateList<IDockable>
            (
                new ToolDock
                {
                    ActiveDockable = moduleExplorer,
                    VisibleDockables = CreateList<IDockable>
                    (
                        moduleExplorer
                    ),
                    Alignment = Alignment.Left,
                    GripMode = GripMode.Visible
                }
            )
        };

        var documentDock = new DocumentDock
        {
            Id = "Documents",
            Title = "Documents",
            IsCollapsable = false,
            Proportion = double.NaN,
            ActiveDockable = hexEditor,
            VisibleDockables = CreateList<IDockable>
            (
                hexEditor
            )
        };

        var windowLayout = CreateRootDock();
        var windowLayoutContent = new ProportionalDock
        {
            Orientation = Orientation.Horizontal,
            IsCollapsable = false,
            VisibleDockables = CreateList<IDockable>
            (
                tools,
                new ProportionalDockSplitter(),
                documentDock
            )
        };

        windowLayout.IsCollapsable = false;
        windowLayout.VisibleDockables = CreateList<IDockable>(windowLayoutContent);
        windowLayout.ActiveDockable = windowLayoutContent;

        var rootDock = CreateRootDock();

        rootDock.IsCollapsable = false;
        rootDock.VisibleDockables = CreateList<IDockable>(windowLayout);
        rootDock.ActiveDockable = windowLayout;
        rootDock.DefaultDockable = windowLayout;

        return rootDock;
    }

    public override void InitLayout(IDockable layout)
    {
        HostWindowLocator = new Dictionary<string, Func<IHostWindow?>>
        {
            [nameof(IDockWindow)] = () => new HostWindow()
        };

        base.InitLayout(layout);
    }
}