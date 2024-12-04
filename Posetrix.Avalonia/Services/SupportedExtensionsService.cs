using System.Collections.Generic;
using Posetrix.Core.Interfaces;

namespace Posetrix.Avalonia.Services;

public class SupportedExtensionsService : IExtensionsService
{
    public List<string> LoadExtensions()
    {
        return
        [
            ".bmp",
            ".jpg",
            ".jpeg",
            ".png",
            ".gif"
        ];
    }
}