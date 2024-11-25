using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;

namespace Posetrix.Core.ViewModels;

public partial class SessionWindowViewModel : BaseViewModel, ICustomWindow
{
    public string WindowTitle => "Drawing session";

    private readonly MainWindowViewModel _mainWindowViewModel;

    private readonly SessionCollection _sessionCollection;

    private readonly ObservableCollection<string> _sessionImages;

    [ObservableProperty]
    private string? _currentImage;

    [ObservableProperty]
    private int _timer;

    [ObservableProperty]
    private int _seconds;



    public SessionWindowViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _sessionCollection = new SessionCollection(_mainWindowViewModel.ReferenceFolders, _mainWindowViewModel.IsShuffleEnabled, _mainWindowViewModel.SessionImageCounter);
        _sessionImages = _sessionCollection.GetImageCollection();
        _currentImage = _sessionImages[0];
    }
}
