using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using Posetrix.Core.Services;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Posetrix.Converters;

/// <summary>
/// A class <c>BitmapImageLoader</c> used for loading images and reducing memory consumption.
/// </summary>
public class BitmapImageLoader : IMultiValueConverter, IDisposable
{
    private BitmapImage? _bitmapImage;
    private FormatConvertedBitmap? _greyscaleBitmap;

    private int _imageResolution;
    private int _imageOrientation = 1; // Default orientation.
    private bool _isGreyScale;

    public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        // Get image quality from settings.
        if (values[1] is int resolution)
        {
            _imageResolution = resolution;
        }

        // Get grayscale setting from MainViewModel.
        if (values[2] is bool isGreyScale)
        {
            _isGreyScale = isGreyScale;
        }

        // Check image path for placeholder images
        if (values[0] is string imagePath && !string.IsNullOrEmpty(imagePath))
        {
            if (imagePath.StartsWith("pack://"))
            {
                return LoadPlaceholder(imagePath);
            }

            return LoadImage(imagePath) ?? LoadPlaceholder(ResourceLocator.Nullmage);
        }

        return LoadPlaceholder(ResourceLocator.WrongPathOrImageData);
    }

    private BitmapImage? LoadPlaceholder(string filePath)
    {
        _bitmapImage = new BitmapImage();
        _bitmapImage.BeginInit();
        _bitmapImage.UriSource = new Uri(filePath, UriKind.RelativeOrAbsolute);

        if (_imageResolution != 0)
        {
            _bitmapImage.DecodePixelWidth = _imageResolution;
        }

        _bitmapImage.CacheOption = BitmapCacheOption.OnDemand;
        _bitmapImage.EndInit();
        _bitmapImage.Freeze();
        return _bitmapImage;
    }

    private ImageSource? LoadImage(string filePath)
    {
        try
        {
            _bitmapImage = new BitmapImage();
            _bitmapImage.BeginInit(); // Starts the initialization process for this element.
            _bitmapImage.UriSource = new Uri(filePath, UriKind.Absolute);

            _imageOrientation = GetImageOrientation(filePath);
            CorrectImageRotation(_bitmapImage, _imageOrientation);

            if (_imageResolution != 0)
            {
                _bitmapImage.DecodePixelWidth = _imageResolution; // Adjust for optimization.
            }

            // Creates a memory store for requested data only.
            // The first request loads the image directly; subsequent requests are filled from the cache.
            _bitmapImage.CacheOption = BitmapCacheOption.OnDemand;

            _bitmapImage.EndInit(); // Indicates that the initialization process for the element is complete.
            _bitmapImage.Freeze(); // Makes the current object unmodifiable and sets its IsFrozen property to true.

            // Convert the image to grayscale if the setting is enabled.
            if (_isGreyScale)
            {
                _greyscaleBitmap = new();
                _greyscaleBitmap.BeginInit();
                _greyscaleBitmap.Source = _bitmapImage;
                _greyscaleBitmap.DestinationFormat = PixelFormats.Gray8; // Converts the image to grayscale.
                _greyscaleBitmap.EndInit();
                _greyscaleBitmap.Freeze();
                return _greyscaleBitmap;
            }

            return _bitmapImage;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Corrects the image rotation based on the EXIF orientation tag.
    /// </summary>
    /// <param name="bitmapImage"></param>
    /// <param name="orientation"></param>
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

    /// <summary>
    /// Gets the image orientation from the EXIF metadata.
    /// </summary>
    /// <param name="imagePath"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
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

    private void DisposeBitmap()
    {
        // Dispose of bitmaps if they are not null.
        _bitmapImage = null;
        _greyscaleBitmap = null;
    }

    public void Dispose()
    {
        DisposeBitmap();
        GC.SuppressFinalize(this);
    }

    ~BitmapImageLoader()
    {
        Dispose();
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}