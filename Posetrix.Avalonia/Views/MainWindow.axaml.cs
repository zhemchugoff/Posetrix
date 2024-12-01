using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;

namespace Posetrix.Avalonia.Views;

public partial class MainWindow : Window, IMainWindow
{
    private IServiceProvider _serviceProvider;
    public MainWindow(IServiceProvider serviceProvider, MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
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

    public string WindowTitle { get; } = "Posetrix Posetrix";
}