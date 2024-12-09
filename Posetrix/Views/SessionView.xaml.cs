using System.Windows;


namespace Posetrix.Views;

/// <summary>
/// Interaction logic for DrawingSessionWindow.xaml
/// </summary>
public partial class SessionView: Window
{
    public SessionView()
    {
        InitializeComponent();
    }
    
    /// <summary>
    /// <c>UpdateScaleTransformCenter</c> ensures the image is loaded and has actual dimensions.
    /// </summary>
    private void UpdateScaleTransformCenter()
    {
        if (SessionImage.ActualWidth > 0 && SessionImage.ActualHeight > 0)
        {
            MirrorTransform.CenterX = SessionImage.ActualWidth / 2;
            MirrorTransform.CenterY = SessionImage.ActualHeight / 2;
        }
    }
    
    private void SessionWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        UpdateScaleTransformCenter();
    }
}