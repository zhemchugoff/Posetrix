using Posetrix.Core.ViewModels;
using System.Windows;
using Posetrix.Views;
using Posetrix.Views.UserControls;
using Microsoft.Extensions.DependencyInjection;

namespace Posetrix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(MainWindowViewModel mainWindowViewModel, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
            DataContext = mainWindowViewModel;

            var userControl = _serviceProvider.GetRequiredService<ReferencesAdd>();
            ReferenceAddControlContainer.Content = userControl;
        }

        private void ShowDrawingSessionWindow_Click(object sender, RoutedEventArgs e)
        {
            DrawingSessionWindow drawingSessionWindow = new DrawingSessionWindow();
            drawingSessionWindow.Show();
        }
    }
}