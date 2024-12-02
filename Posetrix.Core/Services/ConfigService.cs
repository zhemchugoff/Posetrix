using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;

namespace Posetrix.Core.Services;

/// <summary>
/// Class <c>ConfigService</c> provides supported extensions for displaying graphics.
/// </summary>
public class ConfigService : IConfigService
{
    public FileExtensionConfig LoadConfig()
    {
        var fileExtensionConfig = new FileExtensionConfig
        {
            FileExtensions =
            [
                ".bmp",
                ".jpg",
                ".jpeg",
                ".png",
                ".gif",
                ".tiff",
                ".wdp"
            ]
        };   
        return fileExtensionConfig;
    }
}