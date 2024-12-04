using Posetrix.Core.Interfaces;

namespace Posetrix.Avalonia.Services;

public class PlaceHolderImageService: IContentService
{
    public string GetImagePath()
    {
        return "avares://Posetrix.Avalonia/Assets/Images/Happy-Earth-rafiki.png";
    }
}