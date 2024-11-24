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

    private readonly IFolderBrowserService _folderBrowserService;
    private readonly PredefinedIntervalsViewModel _predefinedIntervalsViewModel;
    private readonly CustomIntervalViewModel _customIntervalViewModel;

    public ObservableCollection<ReferencesFolder> ReferenceFolders { get; } = new ObservableCollection<ReferencesFolder>();

    [ObservableProperty]
    public string _folderCount;

    [ObservableProperty]
    private ReferencesFolder? _selectedFolder;

    // ComboBox

    [ObservableProperty]
    private ComboBoxItem? _selectedItem;

    [ObservableProperty]
    private int _sessionImageCounter;

    [ObservableProperty]
    private object? _selectedViewModel;

    public ObservableCollection<ComboBoxViewModel> ViewModelsCollection { get; set; }

    [ObservableProperty]
    private bool _isShuffleEnabled;

    public MainWindowViewModel(IFolderBrowserService folderBrowserService, PredefinedIntervalsViewModel predefinedIntervalsViewModel, CustomIntervalViewModel customIntervalViewModel)
    {
        _folderBrowserService = folderBrowserService;
        _predefinedIntervalsViewModel = predefinedIntervalsViewModel;
        _customIntervalViewModel = customIntervalViewModel;
        SessionImageCounter = 0;
        FolderCount = $"Folders: 0";
        ReferenceFolders.CollectionChanged += ReferenceFolders_CollectionChanged;
        IsShuffleEnabled = true;

        ViewModelsCollection = new ObservableCollection<ComboBoxViewModel>
            {
                new ComboBoxViewModel {ViewModelName = "Predefined intervals", ViewModelObject = _predefinedIntervalsViewModel},
                new ComboBoxViewModel {ViewModelName = "Custom Intervals", ViewModelObject = _customIntervalViewModel}
            };

        SelectedViewModel = ViewModelsCollection[0];
    }

    private void ReferenceFolders_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        FolderCount = $"Folders: {ReferenceFolders.Count}";
    }

    [RelayCommand]
    private void RemoveFolder(ReferencesFolder referencesFolder)
    {
        ReferenceFolders.Remove(referencesFolder);
    }

    [RelayCommand]
    private void OpenFolder()
    {
        var folderPath = _folderBrowserService.OpenFolderDialog();

        if (!string.IsNullOrEmpty(folderPath))
        {
            string folderName = Path.GetFileName(folderPath);
            List<string> references = ReferencesFolder.GetImageFiles(folderPath);

            if (!string.IsNullOrEmpty(folderName) && references.Count > 0 && references is not null)
            {
                ReferencesFolder folderObject = CreateFolderObject(folderPath, folderName, references);

                if (!ReferenceFolders.Contains(folderObject))
                {
                    ReferenceFolders.Add(folderObject);
                }
            }
        }
    }

    private static ReferencesFolder CreateFolderObject(string folderPath, string folderName, List<string> files)
    {
        ReferencesFolder referencesFolder = new ReferencesFolder()
        {
            FullFolderPath = folderPath,
            FolderName = folderName,
            References = files
        };
        return referencesFolder;
    }
}
