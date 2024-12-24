using Posetrix.Core.Interfaces;

namespace Posetrix.Services;

public class ImageResolutionService : IImageResolutionService
{
    private readonly int _fullHD = 1920;
    public int SetResoluton(string quality)
    {
        return quality switch
        {
            "Default" => 0,
            "Low" => _fullHD / 4,
            "Medium" => _fullHD / 2,
            "High" => _fullHD,
            _ => 0
        };
    }
}
