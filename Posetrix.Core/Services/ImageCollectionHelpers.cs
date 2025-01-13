using Posetrix.Core.Models;
using System.Collections.ObjectModel;

namespace Posetrix.Core.Services;

public static class ImageCollectionHelpers
{

    /// <summary>
    /// Method <c>PopulateCollection</c>Get images from folders and add them to a list.
    /// </summary>
    public static List<string> PopulateCollection(ObservableCollection<ImageFolder> imageFolders)
    {
        return imageFolders
        .SelectMany(folder => folder.References ?? [])
        .ToList();
    }

    /// <summary>
    /// Shuffle a list using Fisher-Yates algorithm.
    /// </summary>
    public static void ShuffleCollection(this List<string> images, bool isShuffleEnabled)
    {
        if (isShuffleEnabled)
        {
            Random rng = new();

            int n = images.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rng.Next(0, i + 1);
                // Swap list[i] with the element at random index using tuples.
                (images[j], images[i]) = (images[i], images[j]);
            }
        }
    }

    /// <summary>
    /// Reduce list size if <c>_imageCounter</c> more than 0 and more than <c>_images.Count</c>.
    /// </summary>
    public static void TrimCollectoin(this List<string> images, int imageCounter)
    {
        if (imageCounter > 0 && imageCounter < images.Count)
        {
            images.RemoveRange(imageCounter, images.Count - imageCounter);
        }
    }

    public static void PopulateAndConvertObservableColletionToList(this List<string> paths, ObservableCollection<ImageFolder> folders, bool isShuffleOn, int? imageCount)
    {
        paths.AddRange(PopulateCollection(folders));
        paths.ShuffleCollection(isShuffleOn);
        paths.TrimCollectoin(imageCount ?? 0);
    }

    /// <summary>
    /// Method <c>GetImageFiles</c> populates the list with image files with supported extensions.
    /// </summary>
    public static void GetImageFiles(this List<string> files, string folderPath, List<string> supportedExtensions)
    {
        files.Clear();
        files.AddRange(Directory.GetFiles(folderPath)
            .Where(file => supportedExtensions.Contains(Path.GetExtension(file).ToLowerInvariant())));
    }
}