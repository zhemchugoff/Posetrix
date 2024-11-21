using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;
using Posetrix.Core.Interfaces;

namespace Posetrix.Core.ViewModels;

public partial class FoldersAddWindowViewModel: BaseViewModel
{
    private readonly IFolderBrowserService _folderBrowserService;
    public ObservableCollection<ReferencesFolder> ReferenceFolders { get; set; }

    [ObservableProperty]
    private ReferencesFolder? _selectedFolder;

    public FoldersAddWindowViewModel(IFolderBrowserService folderBrowserService)
    {
        _folderBrowserService = folderBrowserService;
        ReferenceFolders = new ObservableCollection<ReferencesFolder>();
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
        ReferencesFolder referencesFolder  = new ReferencesFolder() { FullFolderPath = folderPath, FolderName = folderName, References = files };
        return referencesFolder;
    }
}
