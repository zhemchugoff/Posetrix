using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Posetrix.Avalonia.Converterts
{
    public class ImageHelper: IValueConverter
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
                    return new Bitmap(filePath);
                }
                catch
                {
                    // Handle invalid image file (optional: return a fallback image or null)
                }
            }

            return null; // Return null if file path is invalid or file does not exist
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}