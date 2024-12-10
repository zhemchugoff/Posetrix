using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using Posetrix.Core.Data;
using Posetrix.Core.Factories;
using Posetrix.Core.Services;


namespace Posetrix.Core.ViewModels;

public partial class MainViewModel : BaseViewModel, ICustomWindow
{
    public string WindowTitle => "Posetrix";
    public string FolderAddTitle => "Add folders";

    private readonly IFolderBrowserServiceAsync _folderBrowserService;
    private readonly IWindowManager _windowManager;
    private readonly ViewModelLocator _viewModelLocator;
    private readonly IExtensionsService _extensionsService;

    public ObservableCollection<ImageFolder> ReferenceFolders { get; } = [];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    [NotifyPropertyChangedFor(nameof(CanStartSession))]
    [NotifyPropertyChangedFor(nameof(CanDeleteFolder))]
    private int _folderCount;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    private int _folderImageCounter;

    public string FoldersInfo => $" Folders: {FolderCount} Images: {FolderImageCounter}";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanDeleteFolder))]
    private ImageFolder? _selectedFolder;

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
    public bool CanDeleteFolder => FolderCount > 0 && SelectedFolder is not null;

    /// <summary>
    /// Design-time only constructor.
    /// </summary>
    public MainViewModel()
    {
    }

    public MainViewModel(IExtensionsService extensionsService,
        IFolderBrowserServiceAsync folderBrowserService, IWindowManager windowManager, ViewModelLocator viewModelLocator)
    {
        _extensionsService = extensionsService;
        _folderBrowserService = folderBrowserService;
        this._windowManager = windowManager;
        this._viewModelLocator = viewModelLocator;

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
    }

    [RelayCommand]
    private void RemoveFolder(ImageFolder referencesFolder)
    {
        Debug.WriteLine(referencesFolder.FolderName);
        ReferenceFolders.Remove(referencesFolder);
        FolderImageCounter -= referencesFolder.ImageCounter;
    }

    [RelayCommand]
    private async Task OpenFolder()
    {
        var folderPath = await _folderBrowserService.SelectFolderAsync();

        if (!string.IsNullOrEmpty(folderPath))
        {
            // Gets a folder name from a full path and removes any trailing separators.
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            var folderName = directoryInfo.Name;

            List<string> references = ImageFolder.GetImageFiles(folderPath, _extensionsService.LoadExtensions());

            if (!string.IsNullOrEmpty(folderName) && references.Count > 0)
            {
                ImageFolder folderObject = CreateFolderObject(folderPath, folderName, references);

                if (!ReferenceFolders.Contains(folderObject))
                {
                    ReferenceFolders.Add(folderObject);
                    FolderImageCounter += folderObject.ImageCounter;
                }
            }
        }
    }

    private static ImageFolder CreateFolderObject(string folderPath, string folderName, List<string> files)
    {
        ImageFolder referencesFolder = new()
        {
            FullFolderPath = folderPath,
            FolderName = folderName,
            References = files
        };
        return referencesFolder;
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
}