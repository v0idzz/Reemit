using ReactiveUI;

namespace Reemit.Gui.ViewModels;

public class HelloViewModel : ReactiveObject, IRoutableViewModel
{
    public string UrlPathSegment => nameof(HelloViewModel);
    
    public IScreen HostScreen { get; }

    public HelloViewModel(IScreen screen) => HostScreen = screen;
}