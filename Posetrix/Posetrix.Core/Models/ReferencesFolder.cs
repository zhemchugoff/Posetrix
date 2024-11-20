namespace Posetrix.Core.Models;

public class ReferencesFolder
{
    public required string FullFolderPath { get; set; }
    public string? FolderName { get; set; }
    public List<String>? References { get; set; }

    public override bool Equals(object? compared)
    {
        // If the variables are located in the same position, they are equal
        if (this == compared)
        {
            return true;
        }

        // If the compared object is null or not of type ReferencesFolder, the objects are not equal
        if ((compared == null) || !this.GetType().Equals(compared.GetType()))
        {
            return false;
        }
        else
        {
            // Convert the object to a ReferencesFolder object
            ReferencesFolder comparedFolder = (ReferencesFolder)compared;
            // If the values of the object variables are equal, the objects are, too
            return this.FullFolderPath.Equals(comparedFolder.FullFolderPath);
        }
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FullFolderPath);
    }
}
