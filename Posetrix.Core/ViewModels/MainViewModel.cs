using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Factories;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;


namespace Posetrix.Core.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    public static string WindowTitle => "Posetrix";

    private readonly IWindowManager _windowManager;
    private readonly ViewModelLocator _viewModelLocator;

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

    // ComboBox.
    public List<IDynamicViewModel> DynamicViews { get; } = [];


    // Number of images, defined by a user. Default is 0: entire collecton.
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(0, int.MaxValue, ErrorMessage = "Enter a correct number")]
    public partial int? SessionImageCount { get; set; } = 0;
    [ObservableProperty] public partial bool IsShuffleEnabled { get; set; } = true;
    [ObservableProperty] public partial bool IsEndlessModeEnabled { get; set; }

    public bool CanStartSession => FolderCount > 0 && SelectedViewModel.CanStart;

    public MainViewModel(IWindowManager windowManager, ViewModelLocator viewModelLocator, ServiceLocator serviceLocator)
    {
        _windowManager = windowManager;
        _viewModelLocator = viewModelLocator;

        // Set app theme on startup.
        var userSettings = serviceLocator.UserSettings;
        var themeService = serviceLocator.ThemeService;
        themeService.SetTheme(userSettings.Theme);

        var customIntervalViewModel = viewModelLocator.CustomIntervalViewModel;
        var predefinedIntervalViewModel = viewModelLocator.PredefinedIntervalsViewModel;

        predefinedIntervalViewModel.ErrorsChanged += (_, _) => StartSessionCommand.NotifyCanExecuteChanged();
        customIntervalViewModel.ErrorsChanged += (_, _) => StartSessionCommand.NotifyCanExecuteChanged();

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
        _windowManager.ShowWindow(_viewModelLocator.SettingsViewModel);
    }

    [RelayCommand]
    private void AddFolders()
    {
        //_windowManager.ShowWindow(_viewModelLocator.FolderAddViewModel);
        _windowManager.ShowDialog(_viewModelLocator.FolderAddViewModel);
    }

    [RelayCommand(CanExecute = nameof(CanStartSession))]
    private void StartSession()
    {
        _windowManager.ShowWindow(_viewModelLocator.SessionViewModel);
    }

    partial void OnSessionImageCountChanged(int? value)
    {
        if (string.IsNullOrWhiteSpace(value.ToString()))
        {
            SessionImageCount = 0;
        }
    }
}