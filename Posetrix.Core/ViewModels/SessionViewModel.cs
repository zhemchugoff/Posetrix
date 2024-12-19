using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;
using Posetrix.Core.Data;
using System.ComponentModel;
using Posetrix.Core.Services;
using System.Diagnostics;

namespace Posetrix.Core.ViewModels;

public partial class SessionViewModel : BaseViewModel, ICustomWindow, IDisposable
{
    public string WindowTitle => "Drawing session";

    private readonly MainViewModel _mainViewModel;
    private readonly ViewModelLocator _viewModelLocator;

    private readonly SynchronizationContext _synchronizationContext;

    // Collections.
    private readonly List<string> _sessionCollection = [];
    private readonly int _sessionCollectionCount;
    private readonly ObservableCollection<string> _completedImages = [];
    private readonly bool _IsEndlessModeOn;

    // Counters.
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SessionInfo))]
    [NotifyCanExecuteChangedFor(nameof(SelectNextImageCommand))]
    [NotifyCanExecuteChangedFor(nameof(SelectPreviousImageCommand))]
    public partial int CurrentImageIndex { get; set; }

    [ObservableProperty] public partial bool IsSessionActive { get; set; } = true;
    [ObservableProperty] public partial bool IsEndOfCollection { get; set; }

    // Commands conditions.
    public bool CanSelectNextImage => IsSessionActive && CurrentImageIndex < _sessionCollectionCount && _sessionCollectionCount > 0;
    public bool CanSelectPreviousImage => IsSessionActive && CurrentImageIndex > 0 && _sessionCollectionCount > 0;
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
    public string SessionInfo => $"{CurrentImageIndex} / {CompletedImagesCounter} / {_sessionCollectionCount}";
    [ObservableProperty] public partial string FormattedTime { get; set; } = "00:00:00";

    // Session disposal.
    //private bool _disposed = false;


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

        _IsEndlessModeOn = _mainViewModel.IsEndlessModeEnabled;

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
        _completedImages.CollectionChanged += CompletedImages_CollectionChanged;
        _sessionCollection.PopulateAndConvertObservableColletionToList(_mainViewModel.ReferenceFolders, _mainViewModel.IsShuffleEnabled, _mainViewModel.ImageCount);
        _sessionCollectionCount = _sessionCollection.Count;

        CurrentImageIndex = 0;
        CurrentImage = _sessionCollection[CurrentImageIndex];
    }

    /// <summary>
    /// Selects next image and increments completed images.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanSelectNextImage))]
    private void SelectNextImage()
    {
        if (!_completedImages.Contains(CurrentImage))
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
        if (CurrentImageIndex == _sessionCollectionCount)
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
            ResetTimer();
        }
    }

    private void SetPlaceholder()
    {
        if (CurrentImageIndex == _sessionCollectionCount)
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

    public void Dispose()
    {
        // TODO: add subscriptions.

        _timerStore.PropertyChanged -= TimerStore_PropertyChanged; // Unsubsribe from property.
        _timerStore.Dispose(); // Dispose of any disposable resources.
        Debug.Write("Disposed!");
        GC.SuppressFinalize(this); // Optionally suppress finalization.
    }
}