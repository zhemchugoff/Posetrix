using System.Windows;
using System.Diagnostics;
using Posetrix.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.ViewModels;
using System.Windows.Controls;
using Posetrix.Core.Models;

namespace Posetrix.Views;

/// <summary>
/// Interaction logic for SettingsWindow.xaml
/// </summary>
public partial class SettingsWindow : Window
{
    public SettingsWindow(SettingsViewModel settingsViewModel)
    {
        InitializeComponent();
        DataContext = settingsViewModel;
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

    private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (ChangeTheme.SelectedItem is Theme theme)
        {
            var selectedValue = theme.Name;

            switch (selectedValue)
            {
                case "Light":
                    Application.Current.ThemeMode = ThemeMode.Light;
                    break;
                case "Dark":
                    Application.Current.ThemeMode = ThemeMode.Dark;
                    break;
                case "System":
                    Application.Current.ThemeMode = ThemeMode.System;
                    break;
                default:
                    break;
            }
        }
    }
}