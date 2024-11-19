using System.Windows;
using Posetrix.Core.ViewModels;
using Posetrix.Services;

namespace Posetrix.Views
{
    /// <summary>
    /// Interaction logic for AddReferenceFoldersWindow.xaml
    /// </summary>
    public partial class ReferenceFoldersAddWindow : Window
    {
        public ReferenceFoldersAddWindow()
        {
            InitializeComponent();
            var folderBrowserService = new FolderBrowserService();
            DataContext = new ReferenceFoldersAddWindowViewModel(folderBrowserService);
        }
    }
}
