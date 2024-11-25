using SkiaSharp;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Posetrix.Converters;

public class SkiaSharpImageLoader : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string imagePath && !string.IsNullOrEmpty(imagePath))
        {
            try
            {
                using (var skBitmap = SKBitmap.Decode(imagePath))
                {
                    return ConvertToBitmapSource(skBitmap);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load image: {ex.Message}");
            }
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private BitmapSource ConvertToBitmapSource(SKBitmap skBitmap)
    {
        // Create a byte array to hold the pixel data.
        int size = skBitmap.Height * skBitmap.Width * 4; // Assuming 4 bytes per pixel (BGRA).
        byte[] pixelData = new byte[size];

        // Copy pixel data from unmanaged memory to managed byte array.
        IntPtr pixels = skBitmap.GetPixels();
        System.Runtime.InteropServices.Marshal.Copy(pixels, pixelData, 0, size);

        // Create the BitmapSource using the pixel data.
        return BitmapSource.Create(
            skBitmap.Width,
            skBitmap.Height,
            96, // DPI X
            96, // DPI Y
            System.Windows.Media.PixelFormats.Bgra32,
            null,
            pixelData,
            skBitmap.RowBytes);
    }
}
