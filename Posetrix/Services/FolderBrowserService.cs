using Microsoft.Win32;
using Posetrix.Core.Interfaces;

namespace Posetrix.Services;

/// <summary>
/// A class <c>FolderBrowserService</c> implements <c>IFolderBrowserService</c> interface for Open Folder Dialog
/// </summary>
public class FolderBrowserService : IFolderBrowserServiceAsync
{
    public Task<string?> SelectFolderAsync()
    {
        var folderDialog = new OpenFolderDialog
        {
            Title = "Select Folder",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        };

        if (folderDialog.ShowDialog() == true)
        {
            var folderFullPath = folderDialog.FolderName;
            return Task.FromResult<string?>(folderFullPath);
        }
        
        return Task.FromResult<string?>(null);
    }
}