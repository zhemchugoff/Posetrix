﻿using System.Collections.ObjectModel;

namespace Posetrix.Core.Models;

public class SessionCollection(ObservableCollection<ImageFolder> imageFolders, bool isShuffleEnabled, int imageCounter)
{
    private readonly List<string> _images = [];

    /// <summary>
    /// Get images from all folders and add them to a list.
    /// </summary>
    public void PopulateCollection()
    {
        foreach (var imageFolder in imageFolders)
        {
            _images.AddRange(from image in imageFolder.References
                             select image);
        }
    }

    /// <summary>
    /// Shuffle the list using Fisher-Yates algorithm.
    /// </summary>
    public void Shuffle()
    {
        Random rng = new();

        int n = _images.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(0, i + 1);
            // Swap list[i] with the element at random index using tuples.
            (_images[j], _images[i]) = (_images[i], _images[j]);
        }
    }

    /// <summary>
    /// Reduce list size if <c>_imageCounter</c> more than 0 and more than <c>_images.Count</c>.
    /// </summary>
    private void TrimList()
    {
        if (imageCounter > 0 && imageCounter < _images.Count)
        {
            _images.RemoveRange(imageCounter, _images.Count - imageCounter);
        }
    }

    /// <summary>
    /// Get populated collection based on provided conditions in the class constructor.
    /// </summary>
    public ObservableCollection<string> GetImageCollection()
    {
        PopulateCollection();


        if (isShuffleEnabled)
        {
            Shuffle();
        }

        TrimList();

        return new ObservableCollection<string>(_images);
    }

}
