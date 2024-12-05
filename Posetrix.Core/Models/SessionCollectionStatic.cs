using System.Collections.ObjectModel;

namespace Posetrix.Core.Models;

public static class SessionCollectionStatic
{
    /// <summary>
    /// Method <c>PopulateCollection</c>Get images from folders and add them to a list.
    /// </summary>
    private static List<string> PopulateCollection(ObservableCollection<ImageFolder> imageFolders)
    {
        List<string> images = [];
        foreach (var imageFolder in imageFolders)
        {
            images.AddRange(from image in imageFolder.References
                select image);
        }

        return images;
    }

    /// <summary>
    /// Shuffle a list using Fisher-Yates algorithm.
    /// </summary>
    private static void Shuffle(List<string> images)
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
    private static void TrimList(int imageCounter, List<string> images)
    {
        if (imageCounter > 0 && imageCounter < images.Count)
        {
            images.RemoveRange(imageCounter, images.Count - imageCounter);
        }
    }

    // TODO: Async?
    /// <summary>
    /// Get populated collection based on provided conditions in the class constructor.
    /// </summary>
    public static ObservableCollection<string> GetImageCollection(ObservableCollection<ImageFolder> imageFolders,
        bool isShuffleEnabled, int imageCounter)
    {
        var imageCollection = PopulateCollection(imageFolders);

        if (isShuffleEnabled)
        {
            Shuffle(imageCollection);
        }

        TrimList(imageCounter, imageCollection);

        return new ObservableCollection<string>(imageCollection);
    }
}