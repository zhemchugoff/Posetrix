using Posetrix.Core.Interfaces;
using System.Collections.Generic;

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