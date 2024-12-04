using Posetrix.Core.Interfaces;

namespace Posetrix.Services;

public class PlaceHolderImageService: IContentService
{
    public string GetImagePath()
    {
        return "Assets/Images/Happy-Earth-bro.png";
    }
}