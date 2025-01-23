using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Enums;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;

namespace Posetrix.Core.ViewModels;

public partial class FolderAddViewModel : BaseViewModel
{
    public static string WindowTitle => "Add folders";
    private readonly IFolderBrowserServiceAsync _folderBrowserService;
    private readonly ISharedCollectionService _sharedCollectionService;

    public ObservableCollection<ImageFolder> Folders { get; }
    private readonly string[] _supportedExtensions;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RemoveFolderCommand))]
    public partial ImageFolder? SelectedFolder { get; set; }

    public bool CanDeleteFolder => SelectedFolder is not null;

    public FolderAddViewModel(IFolderBrowserServiceAsync folderBrowserServiceAsync, IExtensionsService extensionsService, ISharedCollectionService sharedCollectionService)
    {
        ViewModelName = ViewModelNames.FolderAdd;

        _folderBrowserService = folderBrowserServiceAsync;
        _sharedCollectionService = sharedCollectionService;

        _supportedExtensions = extensionsService.LoadExtensions();
        Folders = _sharedCollectionService.ImageFolders;
    }

    [RelayCommand]
    private async Task OpenFolder()
    {
        var folderPaths = await _folderBrowserService.SelectFolderAsync();

        if (folderPaths != null)
        {
            foreach (var folder in from folder in folderPaths
                                   where !string.IsNullOrEmpty(folder)
                                   select folder)
            {
                GetFolderFromPath(folder);
            }
        }
    }

    [RelayCommand(CanExecute = nameof(CanDeleteFolder))]
    private void RemoveFolder(ImageFolder referencesFolder)
    {
        _sharedCollectionService.RemoveFolder(referencesFolder);
    }

    private void GetFolderFromPath(string? folderPath)
    {
        if (string.IsNullOrEmpty(folderPath))
        {
            return;
        }

        DirectoryInfo directoryInfo = new(path: folderPath);
        var folderName = directoryInfo.Name;

        List<string> folderFiles = GetFolderFilesFilteredByExtension(folderPath);

        if (!string.IsNullOrEmpty(folderName) && folderFiles.Count > 0)
        {
            ImageFolder imageFolder = new ImageFolder(folderName, folderPath, folderFiles);
            _sharedCollectionService.AddFolder(imageFolder);
        }
    }

    private List<string> GetFolderFilesFilteredByExtension(string folderPath)
    {
        List<string> files = [];

        foreach (string file in Directory.GetFiles(folderPath))
        {
            if (_supportedExtensions.Contains(Path.GetExtension(file).ToLowerInvariant()))
            {
                files.Add(file);
            }
        }

        return files;
    }
}
