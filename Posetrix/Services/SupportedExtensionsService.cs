using Posetrix.Core.Interfaces;

namespace Posetrix.Services;

public class SupportedExtensionsService : IExtensionsService
{
    public List<string> LoadExtensions()
    {
        return [".bmp", ".jpg", ".jpeg", ".png", ".gif", ".tiff", ".wdp"];
    }
}