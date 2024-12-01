using Posetrix.Core.Interfaces;

namespace Posetrix.Core.Services;

/// <summary>
/// Class <c>ContentService</c> provides shared resources.
/// </summary>
public class ContentService : IContentService
{
    public string GetImagePath()
    {
        var imagePath = "Assets/Enthusiastic-rafiki.png";
        return imagePath;
    }
}
