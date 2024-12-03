using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Posetrix.Core.Interfaces;

namespace Posetrix.Avalonia.Services;

/// <summary>
/// A class <c>FolderBrowserService</c> implements <c>IFolderBrowserServiceAsync</c> interface for Open Folder Dialog
/// </summary>
public class FolderBrowserService : IFolderBrowserServiceAsync
{
    private readonly Func<Window> _windowProvider;

    public FolderBrowserService(Func<Window> windowProvider)
    {
        _windowProvider = windowProvider;
    }

    public async Task<string?> SelectFolderAsync()
    {
        var parentWindow = _windowProvider();
        var storageProvider = parentWindow.StorageProvider;

        if (storageProvider.CanPickFolder)
        {
            var folder = await storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
            {
                Title = "Select a folder",
                AllowMultiple = false // Set to true if you want to allow selecting multiple folders
            });

            if (folder.Count > 0)
            {
                return folder[0].Path.LocalPath;
            }
        }

        return null;
    }
}