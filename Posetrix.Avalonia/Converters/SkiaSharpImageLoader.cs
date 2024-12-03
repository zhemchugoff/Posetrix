using System;
using System.Globalization;
using System.IO;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using SkiaSharp;

namespace Posetrix.Avalonia.Converters;

public class SkiaSharpImageLoader : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string imagePath && !string.IsNullOrEmpty(imagePath))
        {
            // Return a placeholder while the image is being processed
            return LoadImage(imagePath, 1920, 1080);
        }

        return null;
    }
    
    /// <summary>
    /// Method <c>LoadImage</c> loads an image using SkiaSharp, resizes it  and converts to Avalonia bitmap.
    /// </summary>
    private Bitmap LoadImage(string imagePath, int maxWidth, int maxHeight)
    {
        // Load the original image.
        using var skBitmap = SKBitmap.Decode(imagePath);
        
        // Calculate new dimensions.
        var (newWidth, newHeight) = CalculateNewDimensions(skBitmap.Width, skBitmap.Height, maxWidth, maxHeight);
        
        // TODO: fix image rotation from EXIF metadata or XMP metadata
        
        // Resize the image.
        using var resizedBitmap = skBitmap.Resize(
            new SKImageInfo(newWidth, newHeight),
            SKFilterQuality.Medium
        );
        
        if (resizedBitmap == null)
            throw new Exception("Image resizing failed.");
        
        // Convert to Avalonia Bitmap.
        using var skImage = SKImage.FromBitmap(resizedBitmap);
        using var data = skImage.Encode(SKEncodedImageFormat.Jpeg, 100);
        using var stream = new MemoryStream(data.ToArray());
        
        // Return the resized Avalonia bitmap.
        return new Bitmap(stream);
    }

    /// <summary>
    /// Method <c>CalculateNewDimensions</c> calculates the new dimensions using the smaller scale factor to preserve the aspect ratio.
    /// </summary>
    /// <param name="originalWidth"></param>
    /// <param name="originalHeight"></param>
    /// <param name="maxWidth"></param>
    /// <param name="maxHeight"></param>
    /// <returns></returns>
    private static (int Width, int Height) CalculateNewDimensions(int originalWidth, int originalHeight, int maxWidth, int maxHeight)
    {
        // Calculate scaling factors for width and height
        double widthScale = (double)maxWidth / originalWidth;
        double heightScale = (double)maxHeight / originalHeight;

        // Use the smaller scale to maintain aspect ratio
        double scale = Math.Min(widthScale, heightScale);

        // Compute new dimensions
        int newWidth = (int)(originalWidth * scale);
        int newHeight = (int)(originalHeight * scale);

        return (newWidth, newHeight);
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}