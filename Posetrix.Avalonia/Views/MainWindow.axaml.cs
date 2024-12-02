using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;

namespace Posetrix.Avalonia.Views;

public partial class MainWindow : Window, IMainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OpenSettingsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var settingsWindow = App.ServiceProvider.GetRequiredService<SettingsWindow>();
        settingsWindow.DataContext = App.ServiceProvider.GetRequiredService<SettingsViewModel>();
        settingsWindow.Show();
    }
    
    private void AddFolderButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var folderButton = App.ServiceProvider.GetRequiredService<FolderAddWindow>();
        folderButton.Show();
    }

    private void StartNewSession_OnClick(object? sender, RoutedEventArgs e)
    {
        var sessionWindow = App.ServiceProvider.GetRequiredService<SessionWindow>();
        sessionWindow.Show();
    }

    public string WindowTitle { get; } = "Posetrix Posetrix";
}