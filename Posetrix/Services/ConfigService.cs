using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.IO;
using System.Text.Json;

namespace Posetrix.Services;

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
