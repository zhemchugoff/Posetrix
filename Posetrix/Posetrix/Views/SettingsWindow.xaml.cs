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
        public SettingsWindow()
        {
            InitializeComponent();
            DataContext = new SettingsWindowViewModel();
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
