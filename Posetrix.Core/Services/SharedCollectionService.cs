using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;

namespace Posetrix.Core.Services;

public class SharedCollectionService : ISharedCollectionService
{
    public ObservableCollection<ImageFolder> ImageFolders { get; } = [];

    public void AddFolder(ImageFolder imageFolder)
    {
        ImageFolders.Add(imageFolder);
    }

    public void RemoveFolder(ImageFolder imageFolder)
    {
        ImageFolders.Remove(imageFolder);
    }
}

