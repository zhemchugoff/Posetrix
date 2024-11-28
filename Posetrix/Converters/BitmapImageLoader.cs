﻿using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Posetrix.Converters;

/// <summary>
/// A class <c>BitmapImageLoader</c> used for loading images and reducing memory consumption.
/// </summary>
[ValueConversion(typeof(String), typeof(BitmapImage))]
public class BitmapImageLoader : IValueConverter
{
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

                bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
            }
            else
            {
                // Resolve relative paths as pack URIs
                bitmap.UriSource = new Uri($"pack://application:,,,/{filePath}", UriKind.Absolute);
            }

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


    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
