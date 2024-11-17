using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Windows.Media.Imaging;

namespace Posetrix.Converters
{
    public static class ImageLoader
    {
        public static BitmapSource ConvertToBitmapSource(Image<Rgba32> image)
        {
            var width = image.Width;
            var height = image.Height;

            // Create a byte array to hold the pixel data
            var pixelData = new byte[width * height * 4]; // 4 bytes per pixel (BGRA)
            image.CopyPixelDataTo(pixelData);

            // Create a BitmapSource from the pixel data
            var bitmapSource = BitmapSource.Create(
                width,
                height,
                96, // DPI X
                96, // DPI Y
                System.Windows.Media.PixelFormats.Bgra32,
                null,
                pixelData,
                width * 4 // stride
            );

            return bitmapSource;
        }
    }
}
