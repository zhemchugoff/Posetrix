using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace Posetrix.Core.ViewModels;

public partial class MainWindowViewModel : BaseViewModel, ICustomWindow
{
    public string WindowTitle => "Posetrix";

    //private readonly IConfigService _configService;
    private readonly IFolderBrowserService _folderBrowserService;
    private readonly PredefinedIntervalsViewModel _predefinedIntervalsViewModel;
    private readonly CustomIntervalViewModel _customIntervalViewModel;
    private readonly FileExtensionConfig _fileExtensionConfig;

    /// <summary>
    /// A collection of folders with image files.
    /// </summary>
    public ObservableCollection<ImageFolder> ReferenceFolders { get; } = [];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    private int _folderCount;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    private int _folderImageCounter;

    public string FoldersInfo => $" Folders: {FolderCount} Images: {FolderImageCounter}";

    [ObservableProperty]
    private int _customImageCount;

    [ObservableProperty]
    private ImageFolder? _selectedFolder;

    // ComboBox

    [ObservableProperty]
    private ComboBoxItem? _selectedItem;


    [ObservableProperty]
    private object? _selectedViewModel;

    public ObservableCollection<ComboBoxViewModel> ViewModelsCollection { get; set; }

    [ObservableProperty]
    private bool _isShuffleEnabled;

    public MainWindowViewModel(IConfigService configService,IFolderBrowserService folderBrowserService, PredefinedIntervalsViewModel predefinedIntervalsViewModel, CustomIntervalViewModel customIntervalViewModel)
    {
        //_configService = configService;
        _fileExtensionConfig = configService.LoadConfig();
        _folderBrowserService = folderBrowserService;
        _predefinedIntervalsViewModel = predefinedIntervalsViewModel;
        _customIntervalViewModel = customIntervalViewModel;

        // Default value for a folder view.
        FolderCount = 0;
        FolderImageCounter = 0;

        ReferenceFolders.CollectionChanged += ReferenceFolders_CollectionChanged;

        CustomImageCount = 0; // Number of images, defined by a user. Default is 0: endless mode.
        IsShuffleEnabled = true;

        ViewModelsCollection =
            [
                new() {ViewModelName = "Predefined intervals", ViewModelObject = _predefinedIntervalsViewModel},
                new() {ViewModelName = "Custom Intervals", ViewModelObject = _customIntervalViewModel}
            ];

        SelectedViewModel = ViewModelsCollection[0];
    }

    private void ReferenceFolders_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        FolderCount = ReferenceFolders.Count;
    }

    [RelayCommand]
    private void RemoveFolder(ImageFolder referencesFolder)
    {
        ReferenceFolders.Remove(referencesFolder);
        FolderImageCounter -= referencesFolder.ImageCounter;
    }

    [RelayCommand]
    private void OpenFolder()
    {
        var folderPath = _folderBrowserService.OpenFolderDialog();

        if (!string.IsNullOrEmpty(folderPath))
        {
            string folderName = Path.GetFileName(folderPath);
            List<string> references = ImageFolder.GetImageFiles(folderPath, _fileExtensionConfig.FileExtensions);

            if (!string.IsNullOrEmpty(folderName) && references.Count > 0 && references is not null)
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
}
