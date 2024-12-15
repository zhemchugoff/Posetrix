using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Posetrix.Core.Data;

namespace Posetrix.Avalonia.Views;

public partial class SessionView : Window
{
    private Bitmap? DisplayedBitmap { get; set; } = null;

    public SessionView()
    {
        InitializeComponent();
        using Stream stream = Assets.ResourceHelper.GetEmbeddedResourceStream(PlaceHolderService.WindowIcon);
        Icon = new WindowIcon(stream);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        // Assume _displayedBitmap is already assigned elsewhere in your app
        DisplayedBitmap = GetImage();
    }

    private Bitmap? GetImage()
    {
        if (ImageControl.Source is Bitmap bitmap)
        {
            return bitmap;
        }

        return null;
    }
}