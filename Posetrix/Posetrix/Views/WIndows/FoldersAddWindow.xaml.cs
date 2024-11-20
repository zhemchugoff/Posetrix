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
        public FoldersAddWindow()
        {
            InitializeComponent();
            //var folderBrowserService = new FolderBrowserService();
            //DataContext = new ReferenceFoldersAddWindowViewModel(folderBrowserService);
        }
    }
}
