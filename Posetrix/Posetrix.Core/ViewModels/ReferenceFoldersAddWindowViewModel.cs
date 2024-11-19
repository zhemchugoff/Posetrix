using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using Posetrix.Core.Interfaces;

namespace Posetrix.Core.ViewModels;

public partial class ReferenceFoldersAddWindowViewModel: BaseViewModel
{
    private readonly IFolderBrowserService _folderBrowserService;
    public ObservableCollection<ReferencesFolder> ReferenceFolders { get; set; }

    // Supported image extensions that WPF can display out of the box
    private static readonly string[] SupportedImageExtensions =
    [
        ".bmp",
        ".jpg",
        ".jpeg",
        ".png",
        ".gif",
        ".tiff",
        ".wdp"
    ];

    [ObservableProperty]
    private ReferencesFolder? _selectedFolder;

    public ReferenceFoldersAddWindowViewModel(IFolderBrowserService folderBrowserService)
    {
        _folderBrowserService = folderBrowserService;
        ReferenceFolders = new ObservableCollection<ReferencesFolder>();
        //ReferenceFolders = new ObservableCollection<ReferencesFolder>
        //{
        //    new ReferencesFolder {FullFolderPath=@"c:\temp\Folder1", FolderName="Folder 1", References = ["File1", "File2", "File3"] },
        //    new ReferencesFolder {FullFolderPath=@"c:\temp\Folder2", FolderName="Folder 2", References = ["File1", "File2", "File3"] },
        //    new ReferencesFolder {FullFolderPath=@"c:\temp\Folder3", FolderName="Folder 3", References = ["File1", "File2", "File3"] },
        //};
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
            List<string> references = GetImageFiles(folderPath);

            if (!string.IsNullOrEmpty(folderName) && references.Count > 0)
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
        ReferencesFolder referencesFolder  = new ReferencesFolder() { FullFolderPath = folderPath, FolderName = folderName, References = files };
        return referencesFolder;
    }

    private static List<string> GetImageFiles(string folderPath)
    {
        var imageFiles = Directory.GetFiles(folderPath)
            .Where(file => SupportedImageExtensions.Contains(Path.GetExtension(file).ToLowerInvariant()))
            .ToList();

        return imageFiles;
    }
}
