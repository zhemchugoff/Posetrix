using Posetrix.Core.Models;
using Posetrix.Core.Services;
using System.Collections.ObjectModel;

namespace Posetrix.Core.ViewModels
{
    public class FolderAddViewModel: BaseViewModel
    {
        private readonly ViewModelLocator _viewModelLocator;
        public ObservableCollection<ImageFolder> Folders { get; set; }

        public FolderAddViewModel(ViewModelLocator viewModelLocator)
        {
            _viewModelLocator = viewModelLocator;
            var mainViewModel = _viewModelLocator.MainViewModel;
            Folders = mainViewModel.ReferenceFolders;
            //Folders.Add(new ImageFolder { FolderName="Test", FullFolderPath="Test", References=new List<string> {"1.png", "2.png" }  });
        }


    }
}
