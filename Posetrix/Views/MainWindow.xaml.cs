using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.ViewModels;

namespace Posetrix.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowSettingsWindow_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = App.ServiceProvider.GetRequiredService<SettingsWindow>();
            settingsWindow.DataContext = App.ServiceProvider.GetRequiredService<SettingsViewModel>();
            settingsWindow.Show();
        }

        private void AddReferencesButton_Click(object sender, RoutedEventArgs e)
        {
            var foldersAddWindow = App.ServiceProvider.GetRequiredService<FoldersAddWindow>();
            foldersAddWindow.DataContext = App.ServiceProvider.GetService<MainViewModel>();
            foldersAddWindow.Show();
        }

        private void ShowDrawingSessionWindow_Click(object sender, RoutedEventArgs e)
        {
            var drawingSessionWindow = App.ServiceProvider.GetRequiredService<SessionWindow>();
            drawingSessionWindow.DataContext = App.ServiceProvider.GetRequiredService<SessionViewModel>();
            drawingSessionWindow.Show();
        }
    }
}