using Posetrix.Converters;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System.Windows;
using Posetrix.Core.ViewModels;


namespace Posetrix.Views;

/// <summary>
/// Interaction logic for DrawingSessionWindow.xaml
/// </summary>
public partial class SessionWindow : Window
{
    public SessionWindow(SessionWindowViewModel sessionWindowViewModel)
    {
        InitializeComponent();
        DataContext = sessionWindowViewModel;
    }
}
