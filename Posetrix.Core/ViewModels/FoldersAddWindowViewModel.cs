using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;
using Posetrix.Core.Interfaces;
using System.Diagnostics;
using System.Collections.Specialized;

namespace Posetrix.Core.ViewModels;

public partial class FoldersAddWindowViewModel: BaseViewModel
{
    private readonly IFolderBrowserService _folderBrowserService;

    public ObservableCollection<ReferencesFolder> ReferenceFolders { get; } = new ObservableCollection<ReferencesFolder>();

    public string WindowTitle { get; private set; } = "Add your references";

    [ObservableProperty]
    public string _folderCount;

    [ObservableProperty]
    private ReferencesFolder? _selectedFolder;

    public FoldersAddWindowViewModel(IFolderBrowserService folderBrowserService)
    {
        _folderBrowserService = folderBrowserService;
        FolderCount = $"Folders: 0";
        ReferenceFolders.CollectionChanged += ReferenceFolders_CollectionChanged;
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
                    Debug.WriteLine($"Folders {FolderCount}");
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
