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

    private readonly IWindowManager _windowManager;
    private readonly ViewModelLocator _viewModelLocator;

    private CustomIntervalViewModel _cVM;
    private PredefinedIntervalsViewModel _pVM;

    public ObservableCollection<ImageFolder> ReferenceFolders { get; } = [];
    public string FoldersInfo => $" Folders: {FolderCount} Images: {FolderImageCounter}";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    [NotifyCanExecuteChangedFor(nameof(StartSessionCommand))]
    private int _folderCount;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    private int _folderImageCounter;

    // ComboBox.
    public List<IDynamicViewModel> Folders { get; } = new();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartSessionCommand))]
    private IDynamicViewModel _selectedViewModel;

    [ObservableProperty] private int _customImageCount;
    [ObservableProperty] private bool _isShuffleEnabled;

    public bool CanStartSession => FolderCount > 0 && SelectedViewModel.CanStart;

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

        _cVM = viewModelLocator.CustomIntervalViewModel;
        _pVM = viewModelLocator.PredefinedIntervalsViewModel;

        _pVM.ErrorsChanged += (_, _) => StartSessionCommand.NotifyCanExecuteChanged();
        _cVM.ErrorsChanged += (_, _) => StartSessionCommand.NotifyCanExecuteChanged();

        // Folders view.
        FolderCount = 0;
        FolderImageCounter = 0;
        ReferenceFolders.CollectionChanged += ReferenceFolders_CollectionChanged;

        // Combobox.
        Folders.Add(_cVM);
        Folders.Add(_pVM);
        SelectedViewModel = Folders.First();

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
        _windowManager.ShowWindow(_viewModelLocator.FolderAddViewModel);
    }

    [RelayCommand(CanExecute = nameof(CanStartSession))]
    private void StartSession()
    {
        _windowManager.ShowWindow(_viewModelLocator.SessionViewModel);
    }

}