using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using Posetrix.Core.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Posetrix.Core.ViewModels;

public partial class FolderAddViewModel : BaseViewModel, ICustomWindow
{
    public string WindowTitle => "Add folders";

    private readonly ViewModelLocator _viewModelLocator;
    private readonly IFolderBrowserServiceAsync _folderBrowserService;
    private readonly IExtensionsService _extensionsService;
    private readonly ServiceLocator _serviceLocator;
    public int _folderCount;
    public ObservableCollection<ImageFolder> Folders { get; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanDeleteFolder))]
    public partial ImageFolder? SelectedFolder { get; set; }

    public bool CanDeleteFolder => _folderCount > 0 && SelectedFolder is not null;

    public FolderAddViewModel(ViewModelLocator viewModelLocator, ServiceLocator serviceLocator)
    {
        // Services.
        _viewModelLocator = viewModelLocator;
        _serviceLocator = serviceLocator;
        _folderBrowserService = _serviceLocator.FolderBrowserService;
        _extensionsService = _serviceLocator.ExtensionsService;

        // MainViewModel collection.
        var mainViewModel = _viewModelLocator.MainViewModel;
        Folders = mainViewModel.ReferenceFolders;
        Folders.CollectionChanged += Folders_CollectionChanged;
        _folderCount = Folders.Count;
    }

    [RelayCommand]
    private void RemoveFolder(ImageFolder referencesFolder)
    {
        Folders.Remove(referencesFolder);
    }

    [RelayCommand]
    private async Task OpenFolder()
    {
        var folderPath = await _folderBrowserService.SelectFolderAsync();

        if (!string.IsNullOrEmpty(folderPath))
        {
            AddFolder(folderPath);
        }
    }

    private void AddFolder(string? folderPath)
    {
        if (string.IsNullOrEmpty(folderPath))
        {
            return;
        }

        // Gets a folder name from a full path and removes any trailing separators.
        DirectoryInfo directoryInfo = new(path: folderPath);
        var folderName = directoryInfo.Name;

        List<string> references = [];
        references.GetImageFiles(folderPath, _extensionsService.LoadExtensions());

        AddImageFolder(folderPath, folderName, references);
    }

    private void AddImageFolder(string folderPath, string folderName, List<string> references)
    {
        if (!string.IsNullOrEmpty(folderName) && references.Count > 0)
        {
            ImageFolder imageFolder = new(folderPath, folderName, references);

            if (!Folders.Contains(imageFolder))
            {
                Folders.Add(imageFolder);
            }
        }
    }

    private void Folders_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        _folderCount = Folders.Count;
    }
}
