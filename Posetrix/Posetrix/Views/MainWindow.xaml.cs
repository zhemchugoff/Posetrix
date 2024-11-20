using Posetrix.Core.ViewModels;
using System.Windows;
using Posetrix.Views;

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
        }

        private void ShowDrawingSessionWindow_Click(object sender, RoutedEventArgs e)
        {
            DrawingSessionWindow drawingSessionWindow = new DrawingSessionWindow();
            drawingSessionWindow.Show();
        }
    }
}