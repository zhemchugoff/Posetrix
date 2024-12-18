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

    private readonly SynchronizationContext _synchronizationContext;

    // Collections.
    private readonly ObservableCollection<string> _sessionImages = [];
    private readonly ObservableCollection<string> _completedImages = [];

    // Counters.
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SessionInfo))]
    [NotifyCanExecuteChangedFor(nameof(SelectNextImageCommand))]
    [NotifyCanExecuteChangedFor(nameof(SelectPreviousImageCommand))]
    public partial int CurrentImageIndex { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SessionInfo))]
    [NotifyCanExecuteChangedFor(nameof(SelectNextImageCommand))]
    [NotifyCanExecuteChangedFor(nameof(SelectPreviousImageCommand))]
    public partial int SessionCollectionCount { get; set; }

    [ObservableProperty] public partial bool IsSessionActive { get; set; } = true;

    [ObservableProperty] public partial bool IsEndOfCollection { get; set; }

    // Commands conditions.
    public bool CanSelectNextImage => IsSessionActive && CurrentImageIndex < SessionCollectionCount && SessionCollectionCount > 0;
    public bool CanSelectPreviousImage => IsSessionActive && CurrentImageIndex > 0 && SessionCollectionCount > 0;
    public bool IsStopEnabled => IsSessionActive;
    public bool IsPauseEnabled => IsSessionActive;
    public bool IsTimerPaused => _timerStore.IsTimerPaused;

    // Image properties.
    [ObservableProperty] public partial string? CurrentImage { get; set; }
    [ObservableProperty] public partial bool IsMirroredX { get; set; }
    [ObservableProperty] public partial bool IsMirroredY { get; set; }

    // Timer.
    private readonly TimerStore _timerStore;

    [ObservableProperty] public partial int Seconds { get; set; }

    // Session information topbar.
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SessionInfo))]
    public partial int CompletedImagesCounter { get; set; }
    public string SessionInfo => $"{CurrentImageIndex} / {CompletedImagesCounter} / {SessionCollectionCount}";
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

        // Set current UI thread.
        _synchronizationContext = SynchronizationContext.Current
            ?? throw new InvalidOperationException("No SynchronizationContext. Ensure the ViewModel is initialized on the UI thread.");

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
        _sessionImages.CollectionChanged += SessionImages_CollectionChanged;
        _completedImages.CollectionChanged += CompletedImages_CollectionChanged;
        PopulateImageCollection(_sessionImages);

        CurrentImageIndex = 0;
        CurrentImage = _sessionImages[CurrentImageIndex];
    }

    private ObservableCollection<string> PopulateImageCollection(ObservableCollection<string> collection)
    {
        List<string> tempList = ImageCollectionHelpers.PopulateCollection(_mainViewModel.ReferenceFolders);
        tempList.ShuffleCollection(_mainViewModel.IsShuffleEnabled);
        tempList.TrimCollectoin(_mainViewModel.ImageCount ?? 0);
        tempList.ForEach(collection.Add);
        return collection;
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
        CurrentImage = PlaceHolderService.CelebrationImage;
        StopTimer();
        IsSessionActive = false;
    }

    /// <summary>
    /// Checks statuses for view buttons.
    /// </summary>
    private void ResetImageProperties()
    {
        IsMirroredX = false;
        IsMirroredY = false;
    }

    /// <summary>
    /// Updates <c>FormattedTime</c> textbox with formatted text.
    /// </summary>
    /// <param name="remainingTime"></param>
    private void OnTimeUpdated(TimeSpan remainingTime)
    {
        FormattedTime = remainingTime.ToString(@"hh\:mm\:ss");
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
            SetPlaceholder();
        }
        else
        {
            CurrentImage = _sessionImages[value];
            ResetTimer();
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

    private void SessionImages_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        SessionCollectionCount = _sessionImages.Count;
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