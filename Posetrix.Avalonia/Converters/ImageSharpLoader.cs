using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.Processing;
using System;
using System.Globalization;
using System.IO;

namespace Posetrix.Avalonia.Converters;

public class ImageSharpLoader : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string imagePath && !string.IsNullOrEmpty(imagePath))
        {
            return DownscaleImage(imagePath, 1920, 1080);
        }

        return null;
    }

    private static Bitmap DownscaleImage(string imagePath, int maxWidth, int maxHeight)
    {

        using var image = Image.Load(imagePath);

        FixOrientation(image);

        // Calculate new dimensions to maintain aspect ratio.
        var (newWidth, newHeight) = CalculateNewDimensions(image.Width, image.Height, maxWidth, maxHeight);

        // Resize the image.
        image.Mutate(x => x.Resize(newWidth, newHeight));

        // Convert ImageSharp image to Avalonia Bitmap.
        using var stream = new MemoryStream();

        // Set JPEG encoder options to adjust quality.
        var jpegEncoder = new JpegEncoder
        {
            Quality = 100 // Set desired quality (1-100).
        };

        // Save the image to a memory stream with the adjusted quality.
        image.Save(stream, jpegEncoder);
        stream.Seek(0, SeekOrigin.Begin); // Reset the stream position.
        return new Bitmap(stream); // Return an Avalonia Bitmap.
    }

    private static (int Width, int Height) CalculateNewDimensions(int originalWidth, int originalHeight,
        int maxWidth,
        int maxHeight)
    {
        double widthScale = (double)maxWidth / originalWidth;
        double heightScale = (double)maxHeight / originalHeight;
        double scale = Math.Min(widthScale, heightScale);

        int newWidth = (int)(originalWidth * scale);
        int newHeight = (int)(originalHeight * scale);

        return (newWidth, newHeight);
    }

    /// <summary>
    /// Method <c>FixOrientation</c> corrects orientation based on EXIF metadata.
    /// </summary>
    /// <param name="image"></param>
    private static void FixOrientation(Image image)
    {
        if (image.Metadata.ExifProfile != null)
        {
            // Attempt to retrieve the orientation value
            if (image.Metadata.ExifProfile.TryGetValue(ExifTag.Orientation, out var orientationValue) &&
                orientationValue?.GetValue() is ushort orientation)
            {
                switch (orientation)
                {
                    case 2: // Flip horizontal.
                        image.Mutate(x => x.Flip(FlipMode.Horizontal));
                        break;
                    case 3: // Rotate 180.
                        image.Mutate(x => x.Rotate(RotateMode.Rotate180));
                        break;
                    case 4: // Flip vertical.
                        image.Mutate(x => x.Flip(FlipMode.Vertical));
                        break;
                    case 5: // Transpose (rotate 90 + flip horizontal).
                        image.Mutate(x => x.Rotate(RotateMode.Rotate90).Flip(FlipMode.Horizontal));
                        break;
                    case 6: // Rotate 90.
                        image.Mutate(x => x.Rotate(RotateMode.Rotate90));
                        break;
                    case 7: // Transverse (rotate 270 + flip horizontal).
                        image.Mutate(x => x.Rotate(RotateMode.Rotate270).Flip(FlipMode.Horizontal));
                        break;
                    case 8: // Rotate 270.
                        image.Mutate(x => x.Rotate(RotateMode.Rotate270));
                        break;
                }

                // Optionally, remove the orientation tag to prevent future misinterpretation.
                image.Metadata.ExifProfile.RemoveValue(ExifTag.Orientation);
            }
        }
    }


    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}