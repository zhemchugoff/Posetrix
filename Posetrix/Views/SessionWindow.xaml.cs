using System.Windows;
using System.Windows.Media.Imaging;
using Posetrix.Core.ViewModels;


namespace Posetrix.Views;

/// <summary>
/// Interaction logic for DrawingSessionWindow.xaml
/// </summary>
public partial class SessionWindow : Window
{
    public SessionWindow(SessionViewModel sessionViewModel)
    {
        InitializeComponent();
        DataContext = sessionViewModel;
    }
}
