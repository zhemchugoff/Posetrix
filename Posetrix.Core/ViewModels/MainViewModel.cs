using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using Posetrix.Core.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;


namespace Posetrix.Core.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    public static string WindowTitle => "Posetrix";

    private readonly IWindowManager _windowManager;
    private readonly ViewModelLocator _viewModelLocator;

    private readonly CustomIntervalViewModel _cVM;
    private readonly PredefinedIntervalsViewModel _pVM;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartSessionCommand))]
    public partial IDynamicViewModel SelectedViewModel { get; set; }
    public ObservableCollection<ImageFolder> ReferenceFolders { get; } = [];
    public string FoldersInfo => $" Folders: {FolderCount} Images: {FolderImageCounter}";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    [NotifyCanExecuteChangedFor(nameof(StartSessionCommand))]
    public partial int FolderCount { get; set; } = 0;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    public partial int FolderImageCounter { get; set; } = 0;

    // ComboBox.
    public List<IDynamicViewModel> Folders { get; } = [];


    // Number of images, defined by a user. Default is 0: entire collecton.
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(0, int.MaxValue, ErrorMessage = "Enter a correct number")]
    public partial int? ImageCount { get; set; } = 0;
    [ObservableProperty] public partial bool IsShuffleEnabled { get; set; } = true;
    [ObservableProperty] public partial bool IsEndlessModeEnabled { get; set; }

    public bool CanStartSession => FolderCount > 0 && SelectedViewModel.CanStart;

    /// <summary>
    /// Design-time only constructor.
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS9264 // Non-nullable property must contain a non-null value when exiting constructor. Consider adding the 'required' modifier, or declaring the property as nullable, or adding '[field: MaybeNull, AllowNull]' attributes.
    public MainViewModel()
#pragma warning restore CS9264 // Non-nullable property must contain a non-null value when exiting constructor. Consider adding the 'required' modifier, or declaring the property as nullable, or adding '[field: MaybeNull, AllowNull]' attributes.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }

    public MainViewModel(IWindowManager windowManager, ViewModelLocator viewModelLocator, ServiceLocator serviceLocator)
    {
        _windowManager = windowManager;
        _viewModelLocator = viewModelLocator;

        // Set app theme on startup.
        var themeService = serviceLocator.ThemeService;
        var userSettings = serviceLocator.UserSettings;
        themeService.SetTheme(userSettings.Theme);

        _cVM = viewModelLocator.CustomIntervalViewModel;
        _pVM = viewModelLocator.PredefinedIntervalsViewModel;


        _pVM.ErrorsChanged += (_, _) => StartSessionCommand.NotifyCanExecuteChanged();
        _cVM.ErrorsChanged += (_, _) => StartSessionCommand.NotifyCanExecuteChanged();

        ReferenceFolders.CollectionChanged += ReferenceFolders_CollectionChanged;

        // Combobox items.
        Folders.Add(_pVM);
        Folders.Add(_cVM);
        SelectedViewModel = Folders.First();
    }

    private void ReferenceFolders_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        FolderCount = ReferenceFolders.Count;

        if (FolderCount > 0)
        {
            FolderImageCounter = 0; // Reset counter before recounting

            foreach (ImageFolder folder in ReferenceFolders)
            {
                if (folder.References != null)
                {
                    FolderImageCounter += folder.References.Count;
                }
            }
        }
        else
        {
            FolderImageCounter = 0;
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

    partial void OnImageCountChanged(int? value)
    {
        if (string.IsNullOrWhiteSpace(value.ToString()))
        {
            ImageCount = 0;
        }
    }
}