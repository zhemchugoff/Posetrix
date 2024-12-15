using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;
using Posetrix.Core.Data;

namespace Posetrix.Core.ViewModels;

public partial class SessionViewModel : BaseViewModel, ICustomWindow
{
    public string WindowTitle => "Drawing session";

    private readonly MainViewModel _mainViewModel;

    private ObservableCollection<string> _sessionImages;

    private int _currentImageIndex;

    private readonly List<string> _completedImages;

    [ObservableProperty] private bool _canSelectNextImage;
    [ObservableProperty] private bool _canSelectPreviousImage;
    [ObservableProperty] private bool _canDeleteImage;
    [ObservableProperty] private string? _currentImage;

    [ObservableProperty]
    private int _seconds;

    [ObservableProperty]
    private string _formattedTime = "00:00:00";

    private readonly TimerStore _timerStore;

    // Image properties.
    [ObservableProperty] private bool _isMirroredX;
    [ObservableProperty] private bool _isMirroredY;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SessionInfo))]
    private int _completedImagesCounter;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SessionInfo))]
    private int _sessionCollectionCount;

    public string SessionInfo => $"{CompletedImagesCounter} / {SessionCollectionCount}";

    [ObservableProperty]
    private bool _isStopEnabled;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public SessionViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {

    }
    public SessionViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;

        // Timer.
        IDynamicViewModel dynamicView = _mainViewModel.SelectedViewModel;

        _timerStore = new TimerStore(dynamicView.GetSeconds());
        _timerStore.TimeUpdated += OnTimeUpdated;
        _timerStore.Start();

        IsStopEnabled = true;

        PopulateImageCollection();

        _sessionCollectionCount = _sessionImages.Count;
        _completedImages = [];
        _currentImageIndex = 0;
        CurrentImage = _sessionImages[_currentImageIndex];

        UpdateImageStatus();
    }

    private void PopulateImageCollection()
    {
        List<string> tempList = ImageCollectionHelpers.PopulateCollection(_mainViewModel.ReferenceFolders);
        tempList.ShuffleCollection(_mainViewModel.IsShuffleEnabled);
        tempList.TrimCollectoin(_mainViewModel.CustomImageCount);
        _sessionImages = new ObservableCollection<string>(tempList);
    }

    /// <summary>
    /// Selects next image and increments completed images.
    /// </summary>
    [RelayCommand]
    private void SelectNextImage()
    {
        if (CanSelectNextImage)
        {
            if (!_completedImages.Contains(CurrentImage))
            {
                _completedImages.Add(_sessionImages[_currentImageIndex]);
                UpdateImageStatus();
            }

            if (_currentImageIndex != SessionCollectionCount - 1)
            {
                _currentImageIndex++;
                CurrentImage = _sessionImages[_currentImageIndex];
                UpdateImageStatus();
            }
            else
            {
                _currentImageIndex++;
                StopSession();
            }
        }
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
            if (SessionCollectionCount > 1)
            {
                if (_currentImageIndex == SessionCollectionCount - 1)
                {
                    _sessionImages.RemoveAt(_currentImageIndex);
                    _currentImageIndex--;
                    CurrentImage = _sessionImages[_currentImageIndex];
                }
                else
                {
                    _sessionImages.RemoveAt(_currentImageIndex);
                    CurrentImage = _sessionImages[_currentImageIndex];
                }

                UpdateImageStatus();
            }
            else if (SessionCollectionCount == 1)
            {
                _sessionImages.RemoveAt(_currentImageIndex);
                StopSession();
            }
        }
    }

    [RelayCommand]
    private void StopSession()
    {
        CurrentImage = PlaceHolderService.CelebrationImage1;
        IsMirroredX = false;
        IsMirroredY = false;

        //_sessionImages.Clear();
        CanDeleteImage = false;
        CanSelectNextImage = false;
        CanSelectPreviousImage = false;
        IsStopEnabled = false;
    }

    /// <summary>
    /// Checks statuses for view buttons.
    /// </summary>
    private void UpdateImageStatus()
    {
        IsMirroredX = false;
        IsMirroredY = false;

        CompletedImagesCounter = _completedImages.Count;
        SessionCollectionCount = _sessionImages.Count;

        // Checks next image status.
        if (_currentImageIndex < SessionCollectionCount && SessionCollectionCount > 0)
        {
            CanSelectNextImage = true;
        }
        else
        {
            CanSelectNextImage = false;
        }

        // Checks previous image status.
        if (_currentImageIndex > 0 && SessionCollectionCount > 0)
        {
            CanSelectPreviousImage = true;
        }
        else
        {
            CanSelectPreviousImage = false;
        }

        // Checks deletion status.
        CanDeleteImage = SessionCollectionCount > 0;
    }

    /// <summary>
    /// Updates <c>FormattedTime</c> textbox with formatted text.
    /// </summary>
    /// <param name="remainingTime"></param>
    private void OnTimeUpdated(TimeSpan remainingTime)
    {
        FormattedTime = remainingTime.ToString(@"hh\:mm\:ss");
    }

}