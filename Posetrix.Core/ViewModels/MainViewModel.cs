using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Posetrix.Core.Services;


namespace Posetrix.Core.ViewModels;

public partial class MainViewModel : BaseViewModel, ICustomWindow
{
    public string WindowTitle => "Posetrix";
    public string FolderAddTitle => "Add folders";

    private readonly IWindowManager _windowManager;
    private readonly ViewModelLocator _viewModelLocator;

    public ObservableCollection<ImageFolder> ReferenceFolders { get; } = [];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    [NotifyPropertyChangedFor(nameof(CanStartSession))]
    private int _folderCount;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    private int _folderImageCounter;

    public string FoldersInfo => $" Folders: {FolderCount} Images: {FolderImageCounter}";



    // ComboBox.
    public List<string> ViewNamesList { get; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedViewModel))]
    private string _selectedViewName;

    public IDynamicViewModel SelectedViewModel
    {
        get
        {
            return SelectedViewName switch
            {
                "Predefined intervals" => _viewModelLocator.PredefinedIntervalsViewModel,
                "Custom interval" => _viewModelLocator.CustomIntervalViewModel,
                _ => _viewModelLocator.CustomIntervalViewModel,
            };

        }
    }

    [ObservableProperty] private int _customImageCount;
    [ObservableProperty] private bool _isShuffleEnabled;
    public bool CanStartSession => FolderCount > 0;


    /// <summary>
    /// Design-time only constructor.
    /// </summary>
    public MainViewModel()
    {
    }

    public MainViewModel(IWindowManager windowManager, ViewModelLocator viewModelLocator)
    {
        _windowManager = windowManager;
        _viewModelLocator = viewModelLocator;

        // Folders view.
        FolderCount = 0;
        FolderImageCounter = 0;
        ReferenceFolders.CollectionChanged += ReferenceFolders_CollectionChanged;

        // Combobox.
        ViewNamesList = ["Custom interval", "Predefined intervals"];
        SelectedViewName = ViewNamesList.First();

        CustomImageCount = 0; // Number of images, defined by a user. Default is 0: endless mode.

        IsShuffleEnabled = false;

    }

    private void ReferenceFolders_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        FolderCount = ReferenceFolders.Count;

        if (FolderCount > 0)
        {
            foreach (ImageFolder folder in ReferenceFolders)
            {
                FolderImageCounter += folder.References.Count;
            }
        } else
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
    private void StartSession()
    {
        _windowManager.ShowWindow(_viewModelLocator.SessionViewModel);
    }

    [RelayCommand]
    private void AddFolders()
    {
        _windowManager.ShowWindow(_viewModelLocator.FolderAddViewModel);
    }
}