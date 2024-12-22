using Posetrix.Converters;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace Posetrix.Views;

/// <summary>
/// Interaction logic for DrawingSessionWindow.xaml
/// </summary>
public partial class SessionView : Window
{
    public SessionView()
    {
        // TODO: add animation
        InitializeComponent();
        MouseEnter += SessionWindow_MouseEnter;
        MouseLeave += SessionWindow_MouseLeave;
    }

    /// <summary>
    /// <c>UpdateScaleTransformCenter</c> ensures the image is loaded and has actual dimensions.
    /// </summary>
    private void UpdateScaleTransformCenter()
    {
        if (SessionImage.Source != null)
        {
            MirrorTransform.CenterX = SessionImage.ActualWidth / 2;
            MirrorTransform.CenterY = SessionImage.ActualHeight / 2;
        }
    }

    private void SessionWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        UpdateScaleTransformCenter();
    }

    private void SessionImage_Loaded(object sender, RoutedEventArgs e)
    {
        UpdateScaleTransformCenter();
    }

    private void SessionWindow_MouseEnter(object sender, MouseEventArgs e) { SessionStackPanel.Visibility = Visibility.Visible; }
    private void SessionWindow_MouseLeave(object sender, MouseEventArgs e) { SessionStackPanel.Visibility = Visibility.Collapsed; }
}