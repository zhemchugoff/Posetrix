using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace Posetrix.Converters;

/// <summary>
/// A class <c>BitmapImageLoader</c> used for loading images and reducing memory consumption.
/// </summary>
[ValueConversion(typeof(String), typeof(BitmapImage))]
public class BitmapImageLoader : IValueConverter
{
    private int ImageOrientation { get; set; } = 1; // Default orientation.

    /// <summary>
    /// Converts <c>path</c> into a bitmap image.
    /// </summary>
    /// <returns>bitmap image or null</returns>
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string filePath && !string.IsNullOrEmpty(filePath))
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit(); // Starts the initialization process for this element.

            // Convert relative path.
            if (System.IO.Path.IsPathRooted(filePath))
            {
                ImageOrientation = GetImageOrientation(filePath);
                bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
            }
            else
            {
                // Resolve relative paths as pack URIs
                bitmap.UriSource = new Uri($"pack://application:,,,/{filePath}", UriKind.Absolute);
            }

            CorrectImageRotation(bitmap, ImageOrientation);

            bitmap.DecodePixelWidth = 1920; // Adjust for optimization

            // Caches the entire image into memory at load time.
            // All requests for image data are filled from the memory store.
            bitmap.CacheOption = BitmapCacheOption.OnLoad;

            bitmap.EndInit(); // Indicates that the initialization process for the element is complete.
            bitmap.Freeze(); // Makes the current object unmodifiable and sets its IsFrozen property to true.
            return bitmap;
        }

        return null;
    }

    private void CorrectImageRotation(BitmapImage bitmapImage, int orientation)
    {
        switch (orientation)
        {
            case 1: // Normal
                bitmapImage.Rotation = Rotation.Rotate0;
                break;
            case 6: // Rotated 90 degrees clockwise
                bitmapImage.Rotation = Rotation.Rotate90;
                break;
            case 3: // Rotated 180 degrees
                bitmapImage.Rotation = Rotation.Rotate180;
                break;
            case 8: // Rotated 270 degrees clockwise (or 90 degrees counter-clockwise)
                bitmapImage.Rotation = Rotation.Rotate270;
                break;
            default:
                bitmapImage.Rotation = Rotation.Rotate0;
                break;
        }
    }

    private static int GetImageOrientation(string imagePath)
    {
        var directories = ImageMetadataReader.ReadMetadata(imagePath);
        var exifIfd0Directory = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
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