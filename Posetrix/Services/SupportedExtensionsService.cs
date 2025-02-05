﻿using Posetrix.Core.Interfaces;

namespace Posetrix.Services;

public class SupportedExtensionsService : IExtensionsService
{
    public string[] LoadExtensions() => [".bmp", ".jpg", ".jpeg", ".png", ".gif", ".tiff", ".wdp"];
}