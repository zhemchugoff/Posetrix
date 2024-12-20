namespace Posetrix.Core.Models;

public class ImageFolder
{
    public string? FullFolderPath { get; set; }
    public string? FolderName { get; set; }
    public List<string>? References { get; init; }

    public int ImageCounter => References?.Count ?? 0;

    /// <summary>
    /// Gets a folder path and a list of supported extensions.
    /// </summary>
    /// <returns>
    /// Returns a list of image files in a folder with supported extensions.
    /// </returns>
    public static List<string> GetImageFiles(string folderPath, List<string> supportedExtensions)
    {
        var imageFiles = Directory.GetFiles(folderPath)
            .Where(file => supportedExtensions.Contains(Path.GetExtension(file).ToLowerInvariant()))
            .ToList();

        return imageFiles;
    }

    public override bool Equals(object? compared)
    {
        // If the variables are located in the same position, they are equal.
        if (this == compared)
        {
            return true;
        }

        // If the compared object is null or not of type ReferencesFolder, the objects are not equal.
        if ((compared == null) || GetType() != compared.GetType())
        {
            return false;
        }

        // Convert the object to a ReferencesFolder object
        var comparedFolder = (ImageFolder)compared;
        // If the values of the object variables are equal, the objects are, too.
        return FullFolderPath != null && FullFolderPath.Equals(comparedFolder.FullFolderPath);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FullFolderPath, FolderName);
    }
}