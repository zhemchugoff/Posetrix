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

    private readonly List<string> _completedImages;

    [ObservableProperty]
    private bool _canSelectNextImage;

    [ObservableProperty]
    private bool _canSelectPreviousImage;

    [ObservableProperty]
    private bool _canDeleteImage;

    [ObservableProperty]
    private string? _currentImage;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SessionInfo))]
    private int _completedImagesCounter;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SessionInfo))]
    private int _sessionCollectionCount;

    public string SessionInfo => $"Completed: {CompletedImagesCounter} Total: {SessionCollectionCount+1}";

    public SessionWindowViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _sessionCollection = new SessionCollection(_mainWindowViewModel.ReferenceFolders, _mainWindowViewModel.IsShuffleEnabled, _mainWindowViewModel.CustomImageCount);

        _sessionImages = _sessionCollection.GetImageCollection();
        _sessionCollectionCount = _sessionImages.Count;

        _completedImages = [];

        _currentImageIndex = 0;

        CurrentImage = _sessionImages[_currentImageIndex];

        UpdateImageStatus();
    }


    /// <summary>
    /// Selects next image and increments completed images.
    /// </summary>
    [RelayCommand]
    private void SelectNextImage()
    {
        if (CanSelectNextImage)
        {
            _currentImageIndex++;
            CurrentImage = _sessionImages[_currentImageIndex];

            if (!_completedImages.Contains(CurrentImage))
            {
                _completedImages.Add(_sessionImages[_currentImageIndex - 1]);
            }
        }

        UpdateImageStatus();
    }

    [RelayCommand]
    private void SelectPreviousImage()
    {
        if (CanSelectPreviousImage)
        {
            _currentImageIndex--;
            CurrentImage = _sessionImages[_currentImageIndex];
        }
        UpdateImageStatus();
    }


    /// <summary>
    /// Deletes image from session collection.
    /// </summary>
    [RelayCommand]
    private void SkipImage()
    {
        if (CanDeleteImage)
        {
            _sessionImages.RemoveAt(_currentImageIndex);
            CurrentImage = _sessionImages[_currentImageIndex];

        }
        UpdateImageStatus();
    }


    /// <summary>
    /// Checks statuses for view buttons.
    /// </summary>
    private void UpdateImageStatus()
    {

        CompletedImagesCounter = _completedImages.Count;
        SessionCollectionCount = _sessionImages.Count - 1;

        // Checks next image status.
        if (_currentImageIndex < SessionCollectionCount)
        {
            CanSelectNextImage = true;
        }
        else
        {
            CanSelectNextImage = false;
        }

        // Checks previous image status.
        if (_currentImageIndex > 0)
        {
            CanSelectPreviousImage = true;
        }
        else
        {
            CanSelectPreviousImage = false;
        }

        // Checks deletion status.
        if (SessionCollectionCount > _currentImageIndex)
        {
            CanDeleteImage = true;
        }
        else
        {
            CanDeleteImage = false;
        }
    }
}
