using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Posetrix.Core.Models;

public class SessionCollection
{
    private readonly List<string> _images;
    private readonly ObservableCollection<ImageFolder> _imageFolders;
    private readonly bool _isShuffleEnabled;
    private readonly int _imageCounter;

    public SessionCollection(ObservableCollection<ImageFolder> imageFolders, bool isShuffleEnabled, int imageCounter)
    {
        _images = new List<string>();
        _imageFolders = imageFolders;
        _isShuffleEnabled = isShuffleEnabled;
        _imageCounter = imageCounter;
    }


    /// <summary>
    /// Get images from all folders and add them to a list.
    /// </summary>
    public void PopulateCollection()
    {
        foreach (var imageFolder in _imageFolders)
        {
            foreach (var image in imageFolder.References)
            {
                _images.Add(image);
            }
        }
    }


    /// <summary>
    /// Shuffle the list using Fisher-Yates algorithm.
    /// </summary>
    public void Shuffle()
    {
        Random rng = new Random();

        int n = _images.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(0, i + 1);
            // Swap list[i] with the element at random index.
            string temp = _images[i];
            _images[i] = _images[j];
            _images[j] = temp;
        }
    }



    /// <summary>
    /// Reduce list size if <c>_imageCounter</c> more than 0 and more than <c>_images.Count</c>.
    /// </summary>
    private void TrimList()
    {
        if (_imageCounter > 0 && _imageCounter < _images.Count)
        {
            _images.RemoveRange(_imageCounter, _images.Count- _imageCounter);
        }
    }

    /// <summary>
    /// Get populated collection based on provided conditions in the class constructor.
    /// </summary>
    public ObservableCollection<string> GetImageCollection()
    {
        PopulateCollection();


        if (_isShuffleEnabled)
        {
            Shuffle();
        }

        Debug.WriteLine($"Image counter: {_imageCounter} Image count {_images.Count}");
        Debug.WriteLine($"Collection count: {_images.Count}");

        TrimList();

        Debug.WriteLine($"Collection count: {_images.Count}");

        return new ObservableCollection<string>(_images);
    }

}
