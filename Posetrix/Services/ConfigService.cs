using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System;
using System.IO;
using System.Text.Json;

namespace Posetrix.Services;

public class ConfigService: IConfigService
{
    private const string JsonFilePath = "Configs/ImageExtensions.json";

    public FileExtensionConfig LoadConfig()
    {
        if (!File.Exists(JsonFilePath))
        {
            // Handle the case where the config file does not exist
            return new FileExtensionConfig { FileExtensions = [] };
        }

        string? jsonString = File.ReadAllText(JsonFilePath);
        return JsonSerializer.Deserialize<FileExtensionConfig>(jsonString);
    }
}
