using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Posetrix.Avalonia.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OpenSettingsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var settingsWindow = new SettingsWindow();
        settingsWindow.Show();
    }
    
    private void AddFolderButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var folderButton = new FolderAddWindow();
        folderButton.Show();
    }

    private void StartNewSession_OnClick(object? sender, RoutedEventArgs e)
    {
        var sessionWindow = new SessionWindow();
        sessionWindow.Show();
    }
}