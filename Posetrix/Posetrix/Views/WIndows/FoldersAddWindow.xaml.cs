using System.Windows;
using Posetrix.Core.ViewModels;
using Posetrix.Services;

namespace Posetrix.Views
{
    /// <summary>
    /// Interaction logic for AddReferenceFoldersWindow.xaml
    /// </summary>
    public partial class FoldersAddWindow : Window
    {


        public FoldersAddWindow(FoldersAddWindowViewModel foldersAddWindowViewModel)
        {
            InitializeComponent();
            DataContext = foldersAddWindowViewModel;

            //var folderBrowserService = new FolderBrowserService();
            //DataContext = new ReferenceFoldersAddWindowViewModel(folderBrowserService);
        }
    }
}
