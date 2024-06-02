using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reemit.Decompiler;

namespace Reemit.Gui.ViewModels;

public class HelloViewModel : ReactiveObject, IRoutableViewModel
{
    public ReactiveCommand<Unit, Unit> OpenFiles { get; }

    public ReactiveCommand<Unit, Unit> OpenDroppedFiles { get; }

    [Reactive]
    public IReadOnlyList<IStorageItem>? FilesDraggedOver { get; set; }

    [ObservableAsProperty]
    public bool AreAcceptedFilesDraggedOver { get; }

    public string UrlPathSegment => nameof(HelloViewModel);
    
    public IScreen HostScreen { get; }

    public Interaction<Unit, IReadOnlyList<IStorageItem>> ShowOpenFileDialog { get; } = new();

    public HelloViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;

        this.WhenAnyValue(vm => vm.FilesDraggedOver)
            .Select(ShouldAcceptDraggedFiles)
            .ToPropertyEx(this, vm => vm.AreAcceptedFilesDraggedOver);

        OpenFiles = ReactiveCommand.CreateFromTask<Unit, Unit>(OpenFilesAsync);

        var hasAcceptedFilesDraggedOver = this.WhenAnyValue(vm => vm.AreAcceptedFilesDraggedOver);

        OpenDroppedFiles =
            ReactiveCommand.CreateFromTask<Unit, Unit>(OpenDroppedFilesAsync, hasAcceptedFilesDraggedOver);
    }
    
    private static bool ShouldAcceptDraggedFiles(IReadOnlyList<IStorageItem>? items)
    {
        if (items is null)
        {
            return false;
        }
        
        return items.All(i =>
        {
            Span<string> allowedExtensions = [".dll", ".exe"];

            var absolutePath = i.Path.AbsolutePath;
            return allowedExtensions.Contains(Path.GetExtension(absolutePath).ToLower());
        });
    }

    private async Task<Unit> OpenFilesAsync(Unit _)
    {
        var files = await ShowOpenFileDialog.Handle(Unit.Default);
        await OpenFilesAsync(files);

        return Unit.Default;
    }

    private async Task<Unit> OpenDroppedFilesAsync(Unit _)
    {
        await OpenFilesAsync(FilesDraggedOver!);

        return Unit.Default;
    }

    private async Task OpenFilesAsync(IReadOnlyList<IStorageItem> storageItems)
    {
        var clrModules =
            storageItems
                .Select(f => f.Path.AbsolutePath)
                .Select(ClrModule.Open)
                .ToArray();

        await HostScreen.Router.Navigate.Execute(new HomeViewModel(HostScreen, clrModules));
    }
}