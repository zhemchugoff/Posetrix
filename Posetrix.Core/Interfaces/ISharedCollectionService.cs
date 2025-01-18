using Posetrix.Core.Models;
using System.Collections.ObjectModel;

namespace Posetrix.Core.Interfaces;

public interface ISharedCollectionService
{
    ObservableCollection<ImageFolder> ImageFolders { get; }
    void AddFolder(ImageFolder imageFolder);
    void RemoveFolder(ImageFolder imageFolder);
}
