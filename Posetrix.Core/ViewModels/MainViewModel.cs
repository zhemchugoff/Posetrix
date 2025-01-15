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

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartSessionCommand))]
    public partial IDynamicViewModel SelectedViewModel { get; set; }
    public ObservableCollection<ImageFolder> ReferenceFolders { get; } = [];
    public string FoldersInfo => $" Folders: {FolderCount} Images: {ImageCountInfo}";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    [NotifyCanExecuteChangedFor(nameof(StartSessionCommand))]
    public partial int FolderCount { get; set; } = 0;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    public partial int ImageCountInfo { get; set; } = 0;

    public List<IDynamicViewModel> DynamicViews { get; } = [];

    [ObservableProperty] public partial int? SessionImageCount { get; set; } = 0;
    [ObservableProperty] public partial bool IsShuffleEnabled { get; set; } = false;
    [ObservableProperty] public partial bool IsEndlessModeEnabled { get; set; } = false;

    public bool CanStartSession => FolderCount > 0;

    public MainViewModel(IWindowManager windowManager, IViewModelFactory viewModelFactory, IUserSettings userSettings, IThemeService themeService)
    {
        ViewModelName = ViewModelNames.Main;
        _windowManager = windowManager;
        _viewModelFactory = viewModelFactory;

        // Set app theme on startup.
        themeService.SetTheme(userSettings.Theme);

        var customIntervalViewModel = (IDynamicViewModel)_viewModelFactory.GetViewModel(ViewModelNames.CustomInterval);
        var predefinedIntervalViewModel = (IDynamicViewModel)_viewModelFactory.GetViewModel(ViewModelNames.PredefinedIntervals);
        ReferenceFolders.CollectionChanged += ReferenceFolders_CollectionChanged;

        // Combobox items.
        DynamicViews.Add(predefinedIntervalViewModel);
        DynamicViews.Add(customIntervalViewModel);
        SelectedViewModel = DynamicViews.First();
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

    partial void OnSessionImageCountChanged(int? value)
    {
        if (string.IsNullOrWhiteSpace(value.ToString()))
        {
            SessionImageCount = 0;
        }
    }
}