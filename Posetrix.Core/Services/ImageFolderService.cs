using Posetrix.Core.Models;

namespace Posetrix.Core.Data;

/// <summary>
/// Service <c>ImageFolderService</c> is used for creation of <c>ImageFolder</c> object.
/// </summary>
public static class ImageFolderService
{
    public static ImageFolder CreateFolderObject(string folderPath, string folderName, List<string> files)
    {
        ImageFolder referencesFolder = new()
        {
            FullFolderPath = folderPath,
            FolderName = folderName,
            References = files
        };
        return referencesFolder;
    }
}
