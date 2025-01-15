using Posetrix.Core.Models;
using System.Collections.ObjectModel;

namespace Posetrix.Core.Services;

public class ImageCollectionHelpers
{
    /// <summary>
    /// Method <c>PopulateCollection</c> get images from folders and add them to a list.
    /// </summary>
    public static List<string> PopulateListWithImagePaths(List<string> imagePaths, ObservableCollection<ImageFolder> imageFolders)
    {
        //List<string> imagePaths = new List<string>();
        foreach (ImageFolder imageFolder in imageFolders)
        {
            imagePaths.AddRange(imageFolder.References);
        }
        return imagePaths;
    }

    /// <summary>
    /// Shuffle a list using Fisher-Yates algorithm.
    /// </summary>
    public static void ShuffleList(List<string> images)
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

    /// <summary>
    /// Reduce list size if <c>_imageCounter</c> more than 0 and more than <c>_images.Count</c>.
    /// </summary>
    public static void TrimList(List<string> images, int imageCounter)
    {
        if (imageCounter > 0 && imageCounter < images.Count)
        {
            images.RemoveRange(imageCounter, images.Count - imageCounter);
        }
    }

    public static void PopulateAndConvertObservableColletionToList(List<string> paths, ObservableCollection<ImageFolder> folders, bool isShuffleOn, int? imageCount)
    {
        PopulateListWithImagePaths(paths, folders);

        if (isShuffleOn)
        {
            ShuffleList(paths);
        }

        TrimList(paths, imageCount ?? 0);
    }
}