﻿using Posetrix.Core.ViewModels;
using System.Windows;
using Posetrix.Views;
using System.Windows.Controls;

namespace Posetrix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void ShowDrawingSessionWindow_Click(object sender, RoutedEventArgs e)
        {
            DrawingSessionWindow drawingSessionWindow = new DrawingSessionWindow();
            drawingSessionWindow.Show();
        }
    }
}