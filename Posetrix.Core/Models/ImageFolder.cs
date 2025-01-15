namespace Posetrix.Core.Models;

public class ImageFolder(string fullFolderPath, string folderName, List<string> references)
{
    public string FullFolderPath { get; } = fullFolderPath;
    public string FolderName { get; set; } = folderName;
    public List<string> References { get; } = references;

    public int ImageCounter => References.Count;

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

        // Convert the object to ReferencesFolder object.
        var comparedFolder = (ImageFolder)compared;
        // If the values of the object variables are equal, the objects are, too.
        return FullFolderPath != null && FullFolderPath.Equals(comparedFolder.FullFolderPath);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FullFolderPath, FolderName);
    }
}