using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;
using Posetrix.Core.Data;
using System.ComponentModel;
using Posetrix.Core.Services;

namespace Posetrix.Core.ViewModels;

public partial class SessionViewModel : BaseViewModel, ICustomWindow, IDisposable
{
    public string WindowTitle => "Drawing session";

    private readonly MainViewModel _mainViewModel;

    private bool _disposed = false;

    private ObservableCollection<string> _sessionImages;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SelectNextImageCommand))]
    public partial int CurrentImageIndex { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SessionInfo))]
    [NotifyCanExecuteChangedFor(nameof(SelectNextImageCommand))]

    public partial int SessionCollectionCount { get; set; }

    private readonly List<string> _completedImages;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SelectNextImageCommand))]
    public partial bool IsSessionActive { get; set; } = true;

    // Control buttons.
    public bool CanSelectNextImage => CurrentImageIndex < SessionCollectionCount && SessionCollectionCount > 0 && IsSessionActive;

    [ObservableProperty] public partial bool CanSelectPreviousImage { get; set; }
    [ObservableProperty] public partial bool CanDeleteImage { get; set; }
    [ObservableProperty] public partial string? CurrentImage { get; set; }
    [ObservableProperty] public partial bool IsStopEnabled { get; set; } = true;

    [ObservableProperty] public partial bool IsMirroredX { get; set; }
    [ObservableProperty] public partial bool IsMirroredY { get; set; }

    // Timer.
    private readonly TimerStore _timerStore;
    private readonly ViewModelLocator _viewModelLocator;

    public bool IsPaused => _timerStore.IsTimerPaused;

    [ObservableProperty]
    public partial int Seconds { get; set; }

    // Image properties.

    [ObservableProperty] public partial string FormattedTime { get; set; } = "00:00:00";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SessionInfo))]
    public partial int CompletedImagesCounter { get; set; }
    public string SessionInfo => $"{CompletedImagesCounter} / {SessionCollectionCount}";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public SessionViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {

    }
    public SessionViewModel(ViewModelLocator viewModelLocator)
    {
        // Viewmodels.
        _viewModelLocator = viewModelLocator;
        _mainViewModel = _viewModelLocator.MainViewModel;
        IDynamicViewModel dynamicView = _mainViewModel.SelectedViewModel;

        // Timer.
        _timerStore = new TimerStore();
        _timerStore.TimeUpdated += OnTimeUpdated;
        _timerStore.CountdownFinished += OnCountDownFinished;
        _timerStore.PropertyChanged += TimerStore_PropertyChanged; // Subscribe to _timeStore property changes.

        // Create timer.
        Seconds = _mainViewModel.SelectedViewModel.GetSeconds();
        var duration = TimeSpan.FromSeconds(Seconds);
        _timerStore.StartTimer(duration);


        // Image collection.
        PopulateImageCollection();
        SessionCollectionCount = _sessionImages.Count;
        _completedImages = [];
        CurrentImageIndex = 0;
        CurrentImage = _sessionImages[CurrentImageIndex];
        UpdateImageStatus();
    }

    /// <summary>
    /// Methods <c>TimerStore_PropertyChanged</c> updates <c>IsPaused</c> status.
    /// </summary>
    private void TimerStore_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TimerStore.IsTimerPaused))
        {
            OnPropertyChanged(nameof(IsPaused));
        }
    }

    private void OnCountDownFinished()
    {
        SelectNextImage();
    }

    private void PopulateImageCollection()
    {
        List<string> tempList = ImageCollectionHelpers.PopulateCollection(_mainViewModel.ReferenceFolders);
        tempList.ShuffleCollection(_mainViewModel.IsShuffleEnabled);
        tempList.TrimCollectoin(_mainViewModel.ImageCount ?? 0);
        _sessionImages = new ObservableCollection<string>(tempList);
    }

    /// <summary>
    /// Selects next image and increments completed images.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanSelectNextImage))]
    private void SelectNextImage()
    {
        if (CanSelectNextImage)
        {
            if (!_completedImages.Contains(CurrentImage))
            {
                _completedImages.Add(_sessionImages[CurrentImageIndex]);
                UpdateImageStatus();
            }

            if (CurrentImageIndex != SessionCollectionCount - 1)
            {
                CurrentImageIndex++;
                CurrentImage = _sessionImages[CurrentImageIndex];
                ResetTimer();
                UpdateImageStatus();
            }
            else
            {
                CurrentImageIndex++;
                StopSession();
            }
        }
    }

    [RelayCommand]
    private void SelectPreviousImage()
    {
        if (CanSelectPreviousImage)
        {
            CurrentImageIndex--;
            CurrentImage = _sessionImages[CurrentImageIndex];
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
                if (CurrentImageIndex == SessionCollectionCount - 1)
                {
                    _sessionImages.RemoveAt(CurrentImageIndex);
                    CurrentImageIndex--;
                    CurrentImage = _sessionImages[CurrentImageIndex];
                }
                else
                {
                    _sessionImages.RemoveAt(CurrentImageIndex);
                    CurrentImage = _sessionImages[CurrentImageIndex];
                }

                UpdateImageStatus();
            }
            else if (SessionCollectionCount == 1)
            {
                _sessionImages.RemoveAt(CurrentImageIndex);
                StopSession();
            }
        }
    }

    [RelayCommand]
    private void StopSession()
    {
        IsSessionActive = false;
        IsMirroredX = false;
        IsMirroredY = false;
        //IsPaused = false;

        //_sessionImages.Clear();
        CanDeleteImage = false;
        CanSelectPreviousImage = false;
        IsStopEnabled = false;
        CurrentImage = PlaceHolderService.CelebrationImage1;
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

        // Checks previous image status.
        if (CurrentImageIndex > 0 && SessionCollectionCount > 0)
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

    [RelayCommand]
    private void PauseSession()
    {
        if (!IsPaused)
        {
            _timerStore.PauseTimer();
        }
        else
        {
            _timerStore.ResumeTimer();
        }
    }

    private void ResetTimer()
    {
        var duration = TimeSpan.FromSeconds(Seconds);
        _timerStore.ResetTimer(duration);
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _timerStore.PropertyChanged -= TimerStore_PropertyChanged; // Unsubsribe from property.
            _timerStore.Dispose(); // Dispose of any disposable resources.
            _disposed = true; // Mark as disposed.
            GC.SuppressFinalize(this); // Optionally suppress finalization.
        }

    }
}