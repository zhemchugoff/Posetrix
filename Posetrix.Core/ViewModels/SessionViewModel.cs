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
    private readonly ViewModelLocator _viewModelLocator;

    // Collections.
    private ObservableCollection<string> _sessionImages;
    private readonly List<string> _completedImages;

    // Counters.
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SelectNextImageCommand))]
    [NotifyCanExecuteChangedFor(nameof(SelectPreviousImageCommand))]
    public partial int CurrentImageIndex { get; set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SelectNextImageCommand))]
    [NotifyCanExecuteChangedFor(nameof(SelectPreviousImageCommand))]
    [NotifyCanExecuteChangedFor(nameof(SkipImageCommand))]
    [NotifyPropertyChangedFor(nameof(SessionInfo))]
    public partial int SessionCollectionCount { get; set; }

    [ObservableProperty] public partial bool IsSessionActive { get; set; } = true;

    // Commands conditions.
    public bool CanSelectNextImage => CurrentImageIndex < SessionCollectionCount && SessionCollectionCount > 0 && IsSessionActive;
    public bool CanSelectPreviousImage => CurrentImageIndex > 0 && SessionCollectionCount > 0 && IsSessionActive;
    public bool CanDeleteImage => SessionCollectionCount > 0 && IsSessionActive;
    public bool IsStopEnabled => IsSessionActive;
    public bool IsPauseEnabled => IsSessionActive;
    public bool IsTimerPaused => _timerStore.IsTimerPaused;

    // Image properties.
    [ObservableProperty] public partial string? CurrentImage { get; set; }
    [ObservableProperty] public partial bool IsMirroredX { get; set; }
    [ObservableProperty] public partial bool IsMirroredY { get; set; }

    // Timer.
    private readonly TimerStore _timerStore;

    [ObservableProperty]
    public partial int Seconds { get; set; }

    // Session information topbar.
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SessionInfo))]
    public partial int CompletedImagesCounter { get; set; }
    public string SessionInfo => $"{CompletedImagesCounter} / {SessionCollectionCount}";
    [ObservableProperty] public partial string FormattedTime { get; set; } = "00:00:00";

    // Session disposal.
    private bool _disposed = false;


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
            OnPropertyChanged(nameof(IsTimerPaused));
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

    [RelayCommand(CanExecute = nameof(CanSelectPreviousImage))]
    private void SelectPreviousImage()
    {

        CurrentImageIndex--;
        CurrentImage = _sessionImages[CurrentImageIndex];

        UpdateImageStatus();
    }

    /// <summary>
    /// Deletes image from session collection.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanDeleteImage))]
    private void SkipImage()
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

    [RelayCommand(CanExecute = nameof(IsStopEnabled))]
    private void StopSession()
    {
        // TODO: Reset timer to 00:00:00
        CurrentImage = PlaceHolderService.CelebrationImage;
        IsSessionActive = false;
        UpdateImageStatus();
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
    }

    /// <summary>
    /// Updates <c>FormattedTime</c> textbox with formatted text.
    /// </summary>
    /// <param name="remainingTime"></param>
    private void OnTimeUpdated(TimeSpan remainingTime)
    {
        FormattedTime = remainingTime.ToString(@"hh\:mm\:ss");
    }

    [RelayCommand(CanExecute = nameof(IsPauseEnabled))]
    private void PauseSession()
    {
        if (!IsTimerPaused)
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

    /// <summary>
    /// Centralized <c>IsSessionActive</c> notifications for commands.
    /// </summary>
    partial void OnIsSessionActiveChanged(bool oldValue, bool newValue)
    {
        SelectNextImageCommand.NotifyCanExecuteChanged();
        SelectPreviousImageCommand.NotifyCanExecuteChanged();
        PauseSessionCommand.NotifyCanExecuteChanged();
        SkipImageCommand.NotifyCanExecuteChanged();
        StopSessionCommand.NotifyCanExecuteChanged();
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