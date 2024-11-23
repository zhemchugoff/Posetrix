﻿using Posetrix.Core.ViewModels;
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
        //private readonly ICustomWindow _mainWindowViewModel;
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(MainWindowViewModel mainWindowViewModel, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;

            _serviceProvider = serviceProvider;

            var userControl = _serviceProvider.GetRequiredService<ReferencesAdd>();
            ReferenceAddControlContainer.Content = userControl;
        }

        private void ShowDrawingSessionWindow_Click(object sender, RoutedEventArgs e)
        {
            SessionWindow drawingSessionWindow = new SessionWindow();
            drawingSessionWindow.Show();
        }
    }
}