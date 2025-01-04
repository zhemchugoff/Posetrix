using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Data;
using Posetrix.Core.Factories;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using Posetrix.Core.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Posetrix.Core.ViewModels;

public partial class SessionViewModel : BaseViewModel, ICustomWindow, IDisposable
{
    public string WindowTitle => "Drawing session";

    private readonly SynchronizationContext _synchronizationContext;
    private readonly ISoundService _soundService;
    private readonly IUserSettings _userSettings;

    // Collections.
    private readonly List<string> _sessionCollection = [];
    [ObservableProperty] public partial int SessionCollectionCount { get; private set; }
    private readonly ObservableCollection<string> _completedImages = [];
    private readonly bool _IsEndlessModeOn;

    // Counters.
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SessionInfo))]
    [NotifyCanExecuteChangedFor(nameof(SelectNextImageCommand))]
    [NotifyCanExecuteChangedFor(nameof(SelectPreviousImageCommand))]
    public partial int CurrentImageIndex { get; set; }

    [ObservableProperty] public partial bool IsMirroredX { get; private set; } = false;
    [ObservableProperty] public partial bool IsMirroredY { get; private set; } = false;
    [ObservableProperty] public partial bool IsGreyScaleOn { get; set; } = false;
    [ObservableProperty] public partial bool IsAlwaysOnTopOn { get; set; } = false;
    [ObservableProperty] public partial bool IsImageInfoVisible { get; set; } = false;
    [ObservableProperty] public partial bool IsTimeVisible { get; set; }
    [ObservableProperty] public partial bool IsSessionActive { get; set; } = true;
    [ObservableProperty] public partial bool IsEndOfCollection { get; set; }

    // Commands conditions.
    public bool CanSelectNextImage => IsSessionActive && CurrentImageIndex < SessionCollectionCount && SessionCollectionCount > 0;
    public bool CanSelectPreviousImage => IsSessionActive && CurrentImageIndex > 0 && SessionCollectionCount > 0;
    public bool IsStopEnabled => IsSessionActive;
    public bool IsPauseEnabled => IsSessionActive && IsTimeVisible;
    public bool IsTimerPaused => _timerStore.IsTimerPaused && IsTimeVisible;

    // Image properties.
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ImageWidthInfo))]
    public partial double ImageWidth { get; set; } = 0;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ImageHeightInfo))]
    public partial double ImageHeight { get; set; } = 0;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ImagePathInfo))]
    public partial string? CurrentImage { get; set; } = ResourceLocator.DefaultPlaceholder;
    public string ImageWidthInfo => $"Width: {ImageWidth}";
    public string ImageHeightInfo => $"Height: {ImageHeight}";
    public string ImagePathInfo => $"Path: {CurrentImage}";

    // Timer.
    private readonly TimerStore _timerStore;

    [ObservableProperty] public partial int Seconds { get; set; }

    // Session information topbar.
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SessionInfo))]
    public partial int CompletedImagesCounter { get; set; }
    public string SessionInfo => $"{CurrentImageIndex} / {CompletedImagesCounter} / {SessionCollectionCount}";
    [ObservableProperty] public partial string FormattedTime { get; set; } = "00:00:00";

    public int ImageResolution { get; }

    public SessionViewModel(ViewModelLocator viewModelLocator, ServiceLocator serviceLocator)
    {
        // Viewmodels.
        var mainViewModel = viewModelLocator.MainViewModel;
        var dynamicView = mainViewModel.SelectedViewModel;

        _soundService = serviceLocator.SoundService;
        _userSettings = serviceLocator.UserSettings;

        var imageResolutionService = serviceLocator.ImageResolutionService;
        ImageResolution = imageResolutionService.SetResoluton(_userSettings.ImageResolution);

        _IsEndlessModeOn = mainViewModel.IsEndlessModeEnabled;

        // Set current UI thread.
        _synchronizationContext = SynchronizationContext.Current
            ?? throw new InvalidOperationException("No SynchronizationContext. Ensure the ViewModel is initialized on the UI thread.");

        // Timer.
        _timerStore = new TimerStore();
        _timerStore.TimeUpdated += OnTimeUpdated;
        _timerStore.CountdownFinished += OnCountDownFinished;
        _timerStore.PropertyChanged += TimerStore_PropertyChanged; // Subscribe to _timeStore property changes.

        // Create timer.
        Seconds = mainViewModel.SelectedViewModel.GetSeconds();

        if (Seconds == 0)
        {
            IsTimeVisible = false;
            FormattedTime = "";
        }
        else
        {
            IsTimeVisible = true;
            FormattedTime = "00:00:00";
            var duration = TimeSpan.FromSeconds(Seconds);
            _timerStore.StartTimer(duration);
        }

        // Image collection.
        _completedImages.CollectionChanged += CompletedImages_CollectionChanged;
        _sessionCollection.PopulateAndConvertObservableColletionToList(mainViewModel.ReferenceFolders, mainViewModel.IsShuffleEnabled, mainViewModel.SessionImageCount);
        SessionCollectionCount = _sessionCollection.Count;

        CurrentImageIndex = 0;
        CurrentImage = _sessionCollection[CurrentImageIndex];
    }

    /// <summary>
    /// Selects next image and increments completed images.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanSelectNextImage))]
    private void SelectNextImage()
    {
        if (!string.IsNullOrEmpty(CurrentImage) && !_completedImages.Contains(CurrentImage))
        {
            _completedImages.Add(_sessionCollection[CurrentImageIndex]);
        }

        CurrentImageIndex++;
    }

    [RelayCommand(CanExecute = nameof(CanSelectPreviousImage))]
    private void SelectPreviousImage()
    {
        CurrentImageIndex--;
    }

    /// <summary>
    /// Deletes image from session collection.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanSelectNextImage))]
    private void SkipImage()
    {
        CurrentImageIndex++;
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

    [RelayCommand(CanExecute = nameof(IsStopEnabled))]
    private void StopSession()
    {
        ShowEndOfSessionPlaceholder();
    }

    [RelayCommand]
    private void ToggleMirrorX()
    {
        IsMirroredX = !IsMirroredX;
    }

    [RelayCommand]
    private void ToggleMirrorY()
    {
        IsMirroredY = !IsMirroredY;
    }

    [RelayCommand]
    private void ToggleGreyScale()
    {
        IsGreyScaleOn = !IsGreyScaleOn;
    }

    private void OnCountDownFinished()
    {
        if (CanSelectNextImage)
        {
            _synchronizationContext.Post(_ =>
        {
            SelectNextImageCommand.NotifyCanExecuteChanged();

            if (SelectNextImageCommand.CanExecute(null))
            {
                SelectNextImageCommand.Execute(null);
            }
        }, null);
        }
    }

    private void ShowEndOfSessionPlaceholder()
    {
        if (IsTimeVisible)
        {
            StopTimer();
        }

        CurrentImage = ResourceLocator.CelebrationImage;
        IsSessionActive = false;
    }

    /// <summary>
    /// Checks statuses for view buttons.
    /// </summary>
    private void ResetImageProperties()
    {
        IsMirroredX = false;
        IsMirroredY = false;
        IsGreyScaleOn = false;
    }

    /// <summary>
    /// Updates <c>FormattedTime</c> textbox with formatted text.
    /// </summary>
    /// <param name="remainingTime"></param>
    private void OnTimeUpdated(TimeSpan remainingTime)
    {
        FormattedTime = remainingTime.ToString(@"hh\:mm\:ss");

        if (remainingTime == TimeSpan.FromSeconds(3) && _userSettings.Sound != "Off")
        {
            _soundService.PlaySound(_userSettings.Sound);
        }
    }

    private void ResetTimer()
    {
        var duration = TimeSpan.FromSeconds(Seconds);
        _timerStore.ResetTimer(duration);
    }

    private void StopTimer()
    {
        _timerStore.StopTimer();
        FormattedTime = "00:00:00";
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

    partial void OnCurrentImageChanged(string? value)
    {
        ResetImageProperties();
    }

    partial void OnCurrentImageIndexChanged(int value)
    {
        if (CurrentImageIndex == SessionCollectionCount)
        {
            if (_IsEndlessModeOn)
            {
                _sessionCollection.ShuffleCollection(true);
                _completedImages.Clear();
                CurrentImageIndex = 0;
            }
            SetPlaceholder();
        }
        else
        {
            CurrentImage = _sessionCollection[value];

            if (IsTimeVisible)
            {
                ResetTimer();
            }
        }
    }

    private void SetPlaceholder()
    {
        if (CurrentImageIndex == SessionCollectionCount)
        {
            ShowEndOfSessionPlaceholder();
        }
    }

    private void TimerStore_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TimerStore.IsTimerPaused))
        {
            OnPropertyChanged(nameof(IsTimerPaused));
        }
    }

    private void CompletedImages_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        CompletedImagesCounter = _completedImages.Count;
    }
    public void SetImageDimensions(double width, double height)
    {
        ImageWidth = width;
        ImageHeight = height;
    }

    public void Dispose()
    {
        _timerStore.TimeUpdated -= OnTimeUpdated;
        _timerStore.CountdownFinished -= OnCountDownFinished;
        _timerStore.PropertyChanged -= TimerStore_PropertyChanged; // Unsubsribe from property.
        _timerStore.Dispose(); // Dispose of any disposable resources.

        GC.SuppressFinalize(this); // Optionally suppress finalization.
    }

}