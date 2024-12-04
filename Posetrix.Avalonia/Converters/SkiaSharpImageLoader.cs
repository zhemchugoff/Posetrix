using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SkiaSharp;

namespace Posetrix.Avalonia.Converters;

public class SkiaSharpImageLoader : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
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
        // Load Avalonia asset.
        if (imagePath.StartsWith("avares://"))
        {
            try
            {
                using var stream = AssetLoader.Open(new Uri(imagePath));
                var bitmap = new Bitmap(stream);
                bitmap.CreateScaledBitmap(new PixelSize(maxWidth, maxHeight),
                    BitmapInterpolationMode.HighQuality);
                return bitmap;
            }

            catch (Exception e)
            {
                return null;
            }
        }

        // Load the original image.
        using (var skBitmap = SKBitmap.Decode(imagePath))
        {
            var orientation = GetImageOrientation(imagePath);
            var rotetedSkBitmap = AutoOrient(skBitmap, orientation);

            // Calculate new dimensions.
            var (newWidth, newHeight) =
                CalculateNewDimensions(rotetedSkBitmap.Width, rotetedSkBitmap.Height, maxWidth, maxHeight);

            // TODO: add cache for images.

            // Resize the image.
            using var resizedBitmap = rotetedSkBitmap.Resize(
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
    }

    /// <summary>
    /// Method <c>CalculateNewDimensions</c> calculates the new dimensions using the smaller scale factor to preserve the aspect ratio.
    /// </summary>
    private static (int Width, int Height) CalculateNewDimensions(int originalWidth, int originalHeight, int maxWidth,
        int maxHeight)
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

    /// <summary>
    /// Method <c>GetImageOrientation</c> returns an orientation of an encoded image from EXIF data.
    /// </summary>
    public static SKEncodedOrigin GetImageOrientation(string imagePath)
    {
        using (var stream = File.OpenRead(imagePath))
        {
            var codec = SKCodec.Create(stream);
            return codec.EncodedOrigin;
        }
    }

    /// <summary>
    /// Method <c>AutoOrient</c> rotates an image according to EXIF data.
    /// </summary>
    private static SKBitmap AutoOrient(SKBitmap bitmap, SKEncodedOrigin origin)
    {
        // No transformation needed.
        if (origin == SKEncodedOrigin.TopLeft)
        {
            return bitmap;
        }

        SKBitmap rotatedBitmap = new SKBitmap(bitmap.Height, bitmap.Width);

        using (var canvas = new SKCanvas(rotatedBitmap))
        {
            switch (origin)
            {
                case SKEncodedOrigin.BottomRight:
                    canvas.Translate(bitmap.Width, bitmap.Height);
                    canvas.RotateDegrees(180);
                    break;
                case SKEncodedOrigin.RightTop:
                    canvas.Translate(bitmap.Height, 0);
                    canvas.RotateDegrees(90);
                    break;
                case SKEncodedOrigin.LeftBottom:
                    canvas.Translate(0, bitmap.Width);
                    canvas.RotateDegrees(270);
                    break;
                default: return bitmap;
            }

            canvas.DrawBitmap(bitmap, 0, 0);
        }

        bitmap.Dispose();
        return rotatedBitmap;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}