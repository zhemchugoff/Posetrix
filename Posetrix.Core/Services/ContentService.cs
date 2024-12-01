using Posetrix.Core.Interfaces;

namespace Posetrix.Core.Services;

public class ContentService : IContentService
{
    public string GetImagePath()
    {
        var imagePath = "Assets/Enthusiastic-rafiki.png";
        return imagePath;
    }
}
