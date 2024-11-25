using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

    private int _currentImageIndex;

    [ObservableProperty]
    private bool _canSelectNextImage;

    [ObservableProperty]
    private bool _canSelectPreviousImage;

    [ObservableProperty]
    private string? _currentImage;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SessionInfo))]
    private int _sessionImageCounter;

    private readonly int _sessionCollectionCount;

    public string SessionInfo => $"{SessionImageCounter}/{_sessionCollectionCount}";

    public SessionWindowViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _sessionCollection = new SessionCollection(_mainWindowViewModel.ReferenceFolders, _mainWindowViewModel.IsShuffleEnabled, _mainWindowViewModel.SessionImageCounter);

        _sessionImages = _sessionCollection.GetImageCollection();
        _sessionCollectionCount = _sessionImages.Count;

        _currentImageIndex = 0;
        CurrentImage = _sessionImages[_currentImageIndex];

        SessionImageCounter = 1;

        UpdateImageStatus();

    }

    [RelayCommand]
    private void SelectNextImage()
    {
        if (CanSelectNextImage)
        {
            _currentImageIndex++;
            SessionImageCounter++;
            CurrentImage = _sessionImages[_currentImageIndex];
        }
        UpdateImageStatus();
    }

    [RelayCommand]
    private void SelectPreviousImage()
    {
        if (CanSelectPreviousImage)
        {
            _currentImageIndex--;
            SessionImageCounter--;
            CurrentImage = _sessionImages[_currentImageIndex];
        }
        UpdateImageStatus();
    }

    private void UpdateImageStatus()
    {
        if (_currentImageIndex > 0)
        {
            CanSelectPreviousImage = true;
        }
        else
        {
            CanSelectPreviousImage = false;
        }

        if (_currentImageIndex < _sessionImages.Count - 1)
        {
            CanSelectNextImage = true;
        }
        else
        {
            CanSelectNextImage = false;
        }
    }
}
