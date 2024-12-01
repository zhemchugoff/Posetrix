using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;

namespace Posetrix.Core.Services;

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
