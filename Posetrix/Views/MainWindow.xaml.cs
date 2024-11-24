using Posetrix.Core.ViewModels;
using System.Windows;
using Posetrix.Views;
using Posetrix.Views.UserControls;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.Interfaces;

namespace Posetrix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(MainWindowViewModel mainWindowViewModel, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
            _serviceProvider = serviceProvider;
        }

        private void ShowSettingsWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var settingsWindow = _serviceProvider.GetRequiredService<SettingsWindow>();
            settingsWindow.Show();
        }

        private void AddReferencesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var foldersAddWindow = _serviceProvider.GetRequiredService<FoldersAddWindow>();
            foldersAddWindow.Show();
        }
        private void ShowDrawingSessionWindow_Click(object sender, RoutedEventArgs e)
        {
            var drawingSessionWindow = _serviceProvider.GetRequiredService<SessionWindow>();
            drawingSessionWindow.Show();
        }

    }
}