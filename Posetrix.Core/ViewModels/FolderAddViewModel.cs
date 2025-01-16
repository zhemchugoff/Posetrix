using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Enums;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using Posetrix.Core.Services;
using System.Collections.ObjectModel;

namespace Posetrix.Core.ViewModels;

public partial class FolderAddViewModel : BaseViewModel
{
    public static string WindowTitle => "Add folders";
    private readonly IFolderBrowserServiceAsync _folderBrowserService;
    private readonly IExtensionsService _extensionsService;
    public ObservableCollection<ImageFolder> Folders { get; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RemoveFolderCommand))]
    public partial ImageFolder? SelectedFolder { get; set; }

    public bool CanDeleteFolder => SelectedFolder is not null;

    public FolderAddViewModel(IViewModelFactory viewModelFactory, IFolderBrowserServiceAsync folderBrowserServiceAsync, IExtensionsService extensionsService)
    {
        ViewModelName = ViewModelNames.FolderAdd;
        _folderBrowserService = folderBrowserServiceAsync;
        _extensionsService = extensionsService;

        var mainViewModel = viewModelFactory.GetViewModel(ViewModelNames.Main) as MainViewModel ?? throw new InvalidOperationException("MainViewModel is not available.");
        Folders = mainViewModel.ReferenceFolders;
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
                GetFolder(folder);
            }
        }
    }

    [RelayCommand(CanExecute = nameof(CanDeleteFolder))]
    private void RemoveFolder(ImageFolder referencesFolder)
    {
        Folders.Remove(referencesFolder);
    }
    private void GetFolder(string? folderPath)
    {
        if (string.IsNullOrEmpty(folderPath))
        {
            return;
        }

        // Gets a folder name from a full path and removes any trailing separators.
        DirectoryInfo directoryInfo = new(path: folderPath);
        var folderName = directoryInfo.Name;

        List<string> references = [];
        GetImageFiles(references, folderPath, _extensionsService.LoadExtensions());

        AddImageFolderToCollection(folderPath, folderName, references);
    }

    private void AddImageFolderToCollection(string folderPath, string folderName, List<string> references)
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

    /// <summary>
    /// Method <c>GetImageFiles</c> populates the list with image files with supported extensions.
    /// </summary>
    public static void GetImageFiles(List<string> files, string folderPath, List<string> supportedExtensions)
    {
        files.Clear();
        files.AddRange(Directory.GetFiles(folderPath)
            .Where(file => supportedExtensions.Contains(Path.GetExtension(file).ToLowerInvariant())));
    }
}
