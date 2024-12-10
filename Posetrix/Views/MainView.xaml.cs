﻿using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.ViewModels;

namespace Posetrix.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView: Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        //private void ShowSettingsWindow_Click(object sender, RoutedEventArgs e)
        //{
        //    var settingsWindow = App.ServiceProvider.GetRequiredService<SettingsView>();
        //    settingsWindow.DataContext = App.ServiceProvider.GetRequiredService<SettingsViewModel>();
        //    settingsWindow.Show();
        //}

        private void AddReferencesButton_Click(object sender, RoutedEventArgs e)
        {
            var foldersAddWindow = App.ServiceProvider.GetRequiredService<FoldersAddView>();
            foldersAddWindow.DataContext = App.ServiceProvider.GetService<MainViewModel>();
            foldersAddWindow.Show();
        }
    }
}