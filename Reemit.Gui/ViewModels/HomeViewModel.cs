using ReactiveUI;

namespace Reemit.Gui.ViewModels;

public class HomeViewModel : ReactiveObject, IRoutableViewModel
{
    public string UrlPathSegment => nameof(HomeViewModel);
    
    public IScreen HostScreen { get; }

    public HomeViewModel(IScreen screen) => HostScreen = screen;
}