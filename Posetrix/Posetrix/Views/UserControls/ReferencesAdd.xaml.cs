using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;

namespace Posetrix.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ReferencesAdd.xaml
    /// </summary>
    public partial class ReferencesAdd : UserControl
    {
        public ReferencesAdd()
        {
            InitializeComponent();
        }

        private void ShowSettingsWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new();
            settingsWindow.Show();
        }

        private void AddReferencesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FoldersAddWindow referenceFoldersAddWindow = new FoldersAddWindow();
            referenceFoldersAddWindow.Show();
        }
    }
}
