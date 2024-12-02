﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;


namespace Posetrix.Core.ViewModels;

public partial class MainViewModel : BaseViewModel, ICustomWindow
{
    public string WindowTitle => "Posetrix Test";
    public string SessionEndImagePath { get; }

    //private readonly IConfigService _configService;
    private readonly IFolderBrowserServiceAsync _folderBrowserService;
    private readonly FileExtensionConfig _fileExtensionConfig;

    /// <summary>
    /// A collection of folders with image files.
    /// </summary>
    public ObservableCollection<ImageFolder> ReferenceFolders { get; } = [];

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    private int _folderCount;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(FoldersInfo))]
    private int _folderImageCounter;

    public string FoldersInfo => $" Folders: {FolderCount} Images: {FolderImageCounter}";


    [ObservableProperty] private ImageFolder? _selectedFolder;

    // ComboBox
    [ObservableProperty] private ComboBoxViewModel? _selectedViewModel;
    public ObservableCollection<ComboBoxViewModel> ViewModelsCollection { get; set; }

    [ObservableProperty] private int _customImageCount;
    [ObservableProperty] private bool _isShuffleEnabled;

    public MainViewModel(IConfigService configService, IContentService contentService,
        IFolderBrowserServiceAsync folderBrowserService, PredefinedIntervalsViewModel predefinedIntervalsViewModel,
        CustomIntervalViewModel customIntervalViewModel)
    {
        //_configService = configService;
        _fileExtensionConfig = configService.LoadConfig();
        _folderBrowserService = folderBrowserService;

        SessionEndImagePath = contentService.GetImagePath();

        // Default value for a folder view.
        FolderCount = 0;
        FolderImageCounter = 0;

        ReferenceFolders.CollectionChanged += ReferenceFolders_CollectionChanged;
        
        ViewModelsCollection =
        [
            new ComboBoxViewModel { ViewModelName = "Predefined intervals", ViewModelObject = predefinedIntervalsViewModel },
            new ComboBoxViewModel { ViewModelName = "Custom Intervals", ViewModelObject = customIntervalViewModel }
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
            // var folderName = Path.GetFileName(folderPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            Debug.WriteLine($"Folder name: {folderName}");
            List<string> references = ImageFolder.GetImageFiles(folderPath, _fileExtensionConfig.FileExtensions);

            if (!string.IsNullOrEmpty(folderName) && references.Count > 0)
            {
                ImageFolder folderObject = CreateFolderObject(folderPath, folderName, references);

                if (!ReferenceFolders.Contains(folderObject))
                {
                    ReferenceFolders.Add(folderObject);
                    FolderImageCounter += folderObject.ImageCounter;
                    Debug.WriteLine($"Items: {ReferenceFolders.Count}");

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