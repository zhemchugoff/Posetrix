using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Enums;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Posetrix.Core.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    public static string WindowTitle => "Posetrix";

    private readonly IWindowManager _windowManager;
    private readonly IViewModelFactory _viewModelFactory;
    private readonly ISharedCollectionService _sharedCollectionService;
    private readonly ISharedSessionParametersService _sharedSessionParametersService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedView))]
    [NotifyCanExecuteChangedFor(nameof(StartSessionCommand))]
    public partial string SelectedComboboxItem { get; set; }
    public List<string> ComboboxItems { get; } = ["Predefined intervals", "Custom interval (in seconds)"];

    public IDynamicViewModel SelectedView => SelectedComboboxItem == "Predefined intervals"
        ? (IDynamicViewModel)_viewModelFactory.GetViewModel(ViewModelNames.PredefinedIntervals)
        : (IDynamicViewModel)_viewModelFactory.GetViewModel(ViewModelNames.CustomInterval);

    public ObservableCollection<ImageFolder> ReferenceFolders { get; }
    public string FoldersInfo => $" Folders: {FolderCount} Images: {ImageCountInfo}";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    [NotifyCanExecuteChangedFor(nameof(StartSessionCommand))]
    public partial int FolderCount { get; set; } = 0;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    public partial int ImageCountInfo { get; set; } = 0;
    [ObservableProperty] public partial int? SessionImageCount { get; set; }
    [ObservableProperty] public partial bool IsShuffleEnabled { get; set; }
    [ObservableProperty] public partial bool IsEndlessModeEnabled { get; set; }

    public bool CanStartSession => FolderCount > 0;

    public MainViewModel(IWindowManager windowManager, IViewModelFactory viewModelFactory, IUserSettings userSettings, IThemeService themeService, ISharedCollectionService sharedCollectionService, ISharedSessionParametersService sharedSessionParametersService)
    {
        ViewModelName = ViewModelNames.Main;
        themeService.SetTheme(userSettings.Theme);

        _windowManager = windowManager;
        _viewModelFactory = viewModelFactory;
        _sharedCollectionService = sharedCollectionService;
        _sharedSessionParametersService = sharedSessionParametersService;

        SessionImageCount = _sharedSessionParametersService.SessionImageCount;
        IsShuffleEnabled = _sharedSessionParametersService.IsShuffleEnabled;
        IsEndlessModeEnabled = _sharedSessionParametersService.IsEndlessModeEnabled;

        ReferenceFolders = _sharedCollectionService.ImageFolders;
        ReferenceFolders.CollectionChanged += ReferenceFolders_CollectionChanged;

        SelectedComboboxItem = ComboboxItems.First();
    }

    private void ReferenceFolders_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        FolderCount = ReferenceFolders.Count;

        if (FolderCount > 0)
        {
            ImageCountInfo = 0; // Reset counter before recounting.

            foreach (ImageFolder folder in ReferenceFolders)
            {
                if (folder.References != null)
                {
                    ImageCountInfo += folder.References.Count;
                }
            }
        }
        else
        {
            ImageCountInfo = 0;
        }
    }

    [RelayCommand]
    private void OpenSettings()
    {
        _windowManager.ShowDialog(_viewModelFactory.GetViewModel(ViewModelNames.Settings));
    }

    [RelayCommand]
    private void AddFolders()
    {
        _windowManager.ShowDialog(_viewModelFactory.GetViewModel(ViewModelNames.FolderAdd));
    }

    [RelayCommand(CanExecute = nameof(CanStartSession))]
    private void StartSession()
    {
        _windowManager.ShowWindow(_viewModelFactory.GetViewModel(ViewModelNames.Session));
    }

    // Shared parameters update.
    partial void OnSessionImageCountChanged(int? value)
    {
        if (string.IsNullOrWhiteSpace(value.ToString()))
        {
            SessionImageCount = 0;
        }

        _sharedSessionParametersService.SessionImageCount = value;
    }

    partial void OnIsShuffleEnabledChanged(bool value)
    {
        _sharedSessionParametersService.IsShuffleEnabled = value;
    }

    partial void OnIsEndlessModeEnabledChanged(bool value)
    {
        _sharedSessionParametersService.IsEndlessModeEnabled = value;
    }
}