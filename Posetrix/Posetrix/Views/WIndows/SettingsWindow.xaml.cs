using System.Windows;
using Posetrix.Core.ViewModels;
using System.Diagnostics;

namespace Posetrix.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(SettingsWindowViewModel settingsWindowViewModel)
        {
            InitializeComponent();
            DataContext = settingsWindowViewModel;
            Title = settingsWindowViewModel.Title;
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            // Open the URL in the default web browser
            Process.Start(new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });

            e.Handled = true;
        }
    }
}
