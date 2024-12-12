using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using Posetrix.Core.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Posetrix.Core.ViewModels
{
    public partial class FolderAddViewModel : BaseViewModel, ICustomWindow
    {
        public string WindowTitle => "Add folders";

        private readonly ViewModelLocator _viewModelLocator;
        private readonly IFolderBrowserServiceAsync _folderBrowserService;
        private readonly IExtensionsService _extensionsService;

        public int _folderCount;
        public ObservableCollection<ImageFolder> Folders { get;}

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanDeleteFolder))]
        private ImageFolder? _selectedFolder;

        public bool CanDeleteFolder => _folderCount > 0 && SelectedFolder is not null;


        public FolderAddViewModel(ViewModelLocator viewModelLocator, IFolderBrowserServiceAsync folderBrowserService, IExtensionsService extensionsService)
        {
            _viewModelLocator = viewModelLocator;
            _folderBrowserService = folderBrowserService;
            _extensionsService = extensionsService;
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
                // Gets a folder name from a full path and removes any trailing separators.
                DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
                var folderName = directoryInfo.Name;

                List<string> references = ImageFolder.GetImageFiles(folderPath, _extensionsService.LoadExtensions());

                if (!string.IsNullOrEmpty(folderName) && references.Count > 0)
                {
                    ImageFolder folderObject = ImageFolderService.CreateFolderObject(folderPath, folderName, references);

                    if (!Folders.Contains(folderObject))
                    {
                        Folders.Add(folderObject);
                    }
                }
            }
        }

        private void Folders_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _folderCount = Folders.Count;
        }
    }
}
