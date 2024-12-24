using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.Services;
using Posetrix.Core.ViewModels;
using System.IO;

namespace Posetrix.Avalonia.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
        // using Stream stream = Assets.ResourceHelper.GetEmbeddedResourceStream(EmbeddedResourceLocator.WindowIcon);
        // Icon = new WindowIcon(stream);
    }

    // private void OpenSettingsButton_OnClick(object? sender, RoutedEventArgs e)
    // {
    //     var settingsWindow = App.ServiceProvider.GetRequiredService<SettingsView>();
    //     settingsWindow.DataContext = App.ServiceProvider.GetRequiredService<SettingsViewModel>();
    //     settingsWindow.ShowDialog(this);
    // }
    //
    // private void AddFolderButton_OnClick(object? sender, RoutedEventArgs e)
    // {
    //     var foldersAddWindow = App.ServiceProvider.GetRequiredService<FoldersAddView>();
    //     foldersAddWindow.DataContext = App.ServiceProvider.GetRequiredService<MainViewModel>();
    //     foldersAddWindow.ShowDialog(this);
    // }
    //
    // private void StartNewSession_OnClick(object? sender, RoutedEventArgs e)
    // {
    //     var sessionWindow = App.ServiceProvider.GetRequiredService<SessionView>();
    //     sessionWindow.DataContext = App.ServiceProvider.GetRequiredService<SessionViewModel>();
    //     sessionWindow.ShowDialog(this);
    // }
}