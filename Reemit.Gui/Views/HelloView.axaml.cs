using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reemit.Gui.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace Reemit.Gui.Views;

public partial class HelloView : ReactiveUserControl<HelloViewModel>
{
    public HelloView()
    {
        InitializeComponent();

        this.WhenActivated(disposable =>
        {
            this.OneWayBind(ViewModel, vm => vm.IsAcceptedFileDraggedOver, v => v.DragBorderRectangle.IsVisible)
                .DisposeWith(disposable);

            this.OneWayBind(ViewModel, vm => vm.IsAcceptedFileDraggedOver, v => v.OpenFileControlsStackPanel.IsVisible,
                    v => !v)
                .DisposeWith(disposable);

            this.OneWayBind(ViewModel, vm => vm.IsAcceptedFileDraggedOver, v => v.DropHintText.IsVisible)
                .DisposeWith(disposable);

            this.BindCommand(ViewModel, vm => vm.OpenFiles, v => v.PickFileButton)
                .DisposeWith(disposable);

            ViewModel?.ShowOpenFileDialog.RegisterHandler(HandleShowOpenFileDialog)
                .DisposeWith(disposable);

            Observable.FromEventPattern<DragEventArgs>(
                    handler => AddHandler(DragDrop.DragOverEvent, handler),
                    handler => RemoveHandler(DragDrop.DragOverEvent, handler))
                .CombineLatest(ViewModel.WhenAnyValue(vm => vm.IsAcceptedFileDraggedOver))
                .Subscribe(x => x.First.EventArgs.DragEffects = x.Second
                    ? DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Link
                    : DragDropEffects.None)
                .DisposeWith(disposable);

            const string filesDataFormatName = "Files";

            var filesDragEnterObservable = Observable.FromEventPattern<DragEventArgs>(
                    handler => AddHandler(DragDrop.DragEnterEvent, handler),
                    handler => RemoveHandler(DragDrop.DragLeaveEvent, handler))
                .Select(x => x.EventArgs)
                .Select(x => x.Data.Get(filesDataFormatName) as IEnumerable<IStorageItem>)
                .WhereNotNull()
                .Select(x => (IReadOnlyList<IStorageItem>)x.ToArray());

            var filesDragLeaveObservable = Observable.FromEventPattern<DragEventArgs>(
                    handler => AddHandler(DragDrop.DragLeaveEvent, handler),
                    handler => RemoveHandler(DragDrop.DragLeaveEvent, handler))
                .Select(_ => (IReadOnlyList<IStorageItem>?)null);

            filesDragEnterObservable.Merge(filesDragLeaveObservable)
                .BindTo(ViewModel, vm => vm.FilesDraggedOver)
                .DisposeWith(disposable);

            Observable.FromEventPattern<DragEventArgs>(
                    handler => AddHandler(DragDrop.DropEvent, handler),
                    handler => RemoveHandler(DragDrop.DropEvent, handler))
                .Select(_ => Unit.Default)
                .InvokeCommand(ViewModel, vm => vm.OpenDroppedFiles)
                .DisposeWith(disposable);
        });
    }

    private async Task HandleShowOpenFileDialog(
        IInteractionContext<Unit, IReadOnlyList<IStorageItem>> interactionContext)
    {
        var topLevel = TopLevel.GetTopLevel(this);

        if (topLevel is null)
        {
            throw new InvalidOperationException("The control must be attached to a visual tree");
        }

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Choose CLR module",
            AllowMultiple = true,
            FileTypeFilter =
            [
                new FilePickerFileType(".NET Module") { Patterns = ["*.dll", "*.exe"] },
                new FilePickerFileType("All files")
            ]
        });

        interactionContext.SetOutput(files);
    }
}