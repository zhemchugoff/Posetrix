using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;

namespace Posetrix.Core.Services;

/// <summary>
/// Class <c>ConfigService</c> provides supported extensions for displaying graphics.
/// </summary>
public class ConfigService : IConfigService
{
    readonly FileExtensionConfig extensionConfig = new();

    public FileExtensionConfig LoadConfig()
    {
        extensionConfig.FileExtensions =
        [
            ".bmp",
            ".jpg",
            ".jpeg",
            ".png",
            ".gif",
            ".tiff",
            ".wdp"
        ];

        return extensionConfig;
    }
}