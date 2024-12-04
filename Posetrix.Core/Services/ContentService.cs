using Posetrix.Core.Interfaces;

namespace Posetrix.Core.Services;

/// <summary>
/// Class <c>ContentService</c> provides shared resources.
/// </summary>
public class ContentService : IContentService
{
    public string GetImagePath()
    {
        const string imagePath = "avares://Posetrix.Avalonia/Assets/Images/Happy-Earth-rafiki.png";
        return imagePath;
    }
}
