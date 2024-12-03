using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;

namespace Posetrix.Avalonia.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OpenSettingsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var settingsWindow = App.ServiceProvider.GetRequiredService<SettingsWindow>();
        settingsWindow.DataContext = App.ServiceProvider.GetRequiredService<SettingsViewModel>();
        settingsWindow.ShowDialog(this);
    }

    private void AddFolderButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var foldersAddWindow = App.ServiceProvider.GetRequiredService<FolderAddWindow>();
        foldersAddWindow.DataContext = App.ServiceProvider.GetRequiredService<MainViewModel>();
        foldersAddWindow.ShowDialog(this);
    }

    private void StartNewSession_OnClick(object? sender, RoutedEventArgs e)
    {
        var sessionWindow = App.ServiceProvider.GetRequiredService<SessionWindow>();
        sessionWindow.DataContext = App.ServiceProvider.GetRequiredService<SessionViewModel>();
        sessionWindow.ShowDialog(this);
    }
}