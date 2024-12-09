using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using Posetrix.Core.Data;
using Posetrix.Core.Factories;


namespace Posetrix.Core.ViewModels;

public partial class MainViewModel : BaseViewModel, ICustomWindow
{
    public string WindowTitle => "Posetrix";
    public string FolderAddTitle => "Add folders";

    //private readonly IConfigService _configService;
    private readonly IFolderBrowserServiceAsync _folderBrowserService;
    private readonly ModelFactory _modelFactory;
    private readonly IExtensionsService _extensionsService;

    /// <summary>
    /// A collection of folders with image files.
    /// </summary>
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

    // ComboBox
    [ObservableProperty] private ComboBoxViewModel? _selectedViewModel;

    public ObservableCollection<ComboBoxViewModel> ViewModelsCollection { get; set; }

    [ObservableProperty] private int _customImageCount;
    [ObservableProperty] private bool _isShuffleEnabled;
    public bool CanStartSession => FolderCount > 0;
    public bool CanDeleteFolder => FolderCount > 0 && SelectedFolder is not null;

    public CustomIntervalViewModel CustomIntervalVM { get; private set; }
    public PredefinedIntervalsViewModel PredefinedIntervalsVM { get; private set; }

    /// <summary>
    /// Design-time only constructor.
    /// </summary>
    public MainViewModel()
    {
    }

    public MainViewModel(ModelFactory modelFactory, IExtensionsService extensionsService,
        IFolderBrowserServiceAsync folderBrowserService)
    {
        //_configService = configService;
        _modelFactory = modelFactory;
        _extensionsService = extensionsService;
        _folderBrowserService = folderBrowserService;

        // Default value for a folder view.
        FolderCount = 0;
        FolderImageCounter = 0;

        ReferenceFolders.CollectionChanged += ReferenceFolders_CollectionChanged;

        CustomIntervalVM = (CustomIntervalViewModel)_modelFactory.GetViewModelName(ApplicationModelNames.CustomInterval);
        PredefinedIntervalsVM = (PredefinedIntervalsViewModel)_modelFactory.GetViewModelName(ApplicationModelNames.PredefinedIntervals);

        ViewModelsCollection =
        [
            new ComboBoxViewModel
            {
                ViewModelName = "Predefined intervals",
                ViewModelObject = PredefinedIntervalsVM
            },
            new ComboBoxViewModel
            {
                ViewModelName = "Custom Intervals",
                ViewModelObject = CustomIntervalVM
            }
        ];

        SelectedViewModel = ViewModelsCollection.First();

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

    public SessionTimer GetTimer()
    {
        if (SelectedViewModel.ViewModelObject == CustomIntervalVM)
        {
            return new SessionTimer()
            {
                Hours = CustomIntervalVM.Hours,
                Minutes = CustomIntervalVM.Minutes,
                Seconds = CustomIntervalVM.Seconds,
            };
        }

        return new SessionTimer()
        {
            Hours = 0,
            Minutes = 0,
            Seconds = 0,
        };

    }
}