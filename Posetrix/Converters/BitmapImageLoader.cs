using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using Posetrix.Assets;
using Posetrix.Core.Services;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Posetrix.Converters;

/// <summary>
/// A class <c>BitmapImageLoader</c> used for loading images and reducing memory consumption.
/// </summary>
[ValueConversion(typeof(string), typeof(BitmapImage))]
public class BitmapImageLoader : IValueConverter
{
    private readonly int PixelWidth = 1920;
    private int ImageOrientation { get; set; } = 1; // Default orientation.

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string imagePath && !string.IsNullOrEmpty(imagePath))
        {
            if (imagePath.StartsWith("Images."))
            {
                return LoadPlaceholder(imagePath);
            }

            return LoadImage(imagePath) ?? LoadPlaceholder(PlaceHolderService.ErrorImage);
        }

        return LoadPlaceholder(PlaceHolderService.ErrorImage);
    }

    private BitmapImage? LoadPlaceholder(string filePath)
    {
        using Stream stream = ResourceHelper.GetEmbeddedResourceStream(filePath);
        if (stream == null) return null;

        var bmp = new BitmapImage();
        bmp.BeginInit();
        bmp.StreamSource = stream;
        bmp.DecodePixelWidth = PixelWidth;

        bmp.EndInit();
        bmp.Freeze();
        return bmp;
    }

    private BitmapImage? LoadImage(string filePath)
    {
        try
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit(); // Starts the initialization process for this element.
            bitmap.UriSource = new Uri(filePath, UriKind.Absolute);

            ImageOrientation = GetImageOrientation(filePath);
            CorrectImageRotation(bitmap, ImageOrientation);

            bitmap.DecodePixelWidth = PixelWidth; // Adjust for optimization.

            // Caches the entire image into memory at load time.
            // All requests for image data are filled from the memory store.
            bitmap.CacheOption = BitmapCacheOption.OnLoad;

            bitmap.EndInit(); // Indicates that the initialization process for the element is complete.
            bitmap.Freeze(); // Makes the current object unmodifiable and sets its IsFrozen property to true.

            return bitmap;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static void CorrectImageRotation(BitmapImage bitmapImage, int orientation)
    {
        bitmapImage.Rotation = orientation switch
        {
            // Normal
            1 => Rotation.Rotate0,
            // Rotated 90 degrees clockwise
            6 => Rotation.Rotate90,
            // Rotated 180 degrees
            3 => Rotation.Rotate180,
            // Rotated 270 degrees clockwise (or 90 degrees counter-clockwise)
            8 => Rotation.Rotate270,
            _ => Rotation.Rotate0,
        };
    }

    private static int GetImageOrientation(object imagePath)
    {
        IReadOnlyList<MetadataExtractor.Directory> directories;

        if (imagePath is string stringPath)
        {
            directories = ImageMetadataReader.ReadMetadata(stringPath);
        }

        else if (imagePath is Stream streamPath)
        {
            directories = ImageMetadataReader.ReadMetadata(streamPath);
        }

        else
        {
            throw new ArgumentException("Input must be either a string or a stream.");
        }

        ExifIfd0Directory? exifIfd0Directory = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
        if (exifIfd0Directory != null &&
            exifIfd0Directory.TryGetInt32(ExifDirectoryBase.TagOrientation, out int orientation))
        {
            return orientation;
        }

        return 1; // Default orientation if metadata is missing.
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}