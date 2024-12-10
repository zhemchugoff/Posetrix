using Posetrix.Core.Models;

namespace Posetrix.Core.Services
{
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
}
