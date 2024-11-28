using Posetrix.Core.Interfaces;
using System.Windows.Media.Imaging;

namespace Posetrix.Services;

public class ContentService : IContentService
{
    public string GetImagePath()
    {
        var imagePath = "Images/undraw_workout_gcgu.png";
        return imagePath;
    }
}
