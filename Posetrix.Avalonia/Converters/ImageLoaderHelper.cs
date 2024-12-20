using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Globalization;
using System.IO;

namespace Posetrix.Avalonia.Converters;

public class ImageHelper : IValueConverter
{
    public static Bitmap LoadFromResource(Uri resourceUri)
    {
        return new Bitmap(AssetLoader.Open(resourceUri));
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string filePath && !string.IsNullOrEmpty(filePath))
        {
            try
            {
                using var stream = File.OpenRead(filePath);
                var bitmap = new Bitmap(stream);

                bitmap.CreateScaledBitmap(new PixelSize(800, 600), BitmapInterpolationMode.MediumQuality);
                return bitmap;
            }

            catch (Exception e)
            {
                return null;
            }
        }

        return null; // Return null if file path is invalid or file does not exist.
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}