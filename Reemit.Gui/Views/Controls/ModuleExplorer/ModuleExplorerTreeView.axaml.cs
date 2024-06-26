using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reemit.Gui.ViewModels.Controls.ModuleExplorer;

namespace Reemit.Gui.Views.Controls.ModuleExplorer;

public partial class ModuleExplorerTreeView : ReactiveUserControl<ModuleExplorerTreeViewModel>
{
    public ModuleExplorerTreeView()
    {
        InitializeComponent();
        this.WhenActivated(disposable =>
        {
            this.OneWayBind(ViewModel, vm => vm.RootNodes, v => v.TreeView.ItemsSource)
                .DisposeWith(disposable);

            this.Bind(ViewModel, x => x.SelectedNode, x => x.TreeView.SelectedItem)
                .DisposeWith(disposable);

            //this.WhenAnyValue(x => x.TreeView.SelectedItem)
            //    .Do(x =>
            //    {
            //        Console.WriteLine("Selected item changed");
            //    })
            //    .BindTo(ViewModel, vm => vm.SelectedNode)                
            //    .DisposeWith(disposable);
                

            //this.OneWayBind(ViewModel, vm => vm.SelectedNode, v => v.TreeView.SelectedItem)
            //    .DisposeWith(disposable);

            
        });
    }
}