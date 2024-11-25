using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Posetrix.Core.ViewModels;

public partial class SessionWindowViewModel : BaseViewModel, ICustomWindow
{
    public string WindowTitle => "Drawing session";

    private readonly MainWindowViewModel _mainWindowViewModel;

    private readonly SessionCollection _sessionCollection;
    private readonly ObservableCollection<string> _sessionImages;

    private int _currentImageIndex;

    [ObservableProperty]
    private bool _canSelectNextImage;

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
        _currentImageIndex = 0;
        CurrentImage = _sessionImages[_currentImageIndex];
        UpdateNextImageStatus();
    }

    [RelayCommand]
    private void SelectNextImage()
    {
        if (CanSelectNextImage)
        {
            _currentImageIndex++;
            CurrentImage = _sessionImages[_currentImageIndex];
        }
        UpdateNextImageStatus();
    }

    private void UpdateNextImageStatus()
    {
        if (_currentImageIndex < _sessionImages.Count-1)
        {
            CanSelectNextImage = true;
        }
        else
        {
            CanSelectNextImage = false;
        }
    }
}
