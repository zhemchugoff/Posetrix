namespace Posetrix.Core.Interfaces;

public interface IFolderBrowserServiceAsync
{
    Task<string[]?> SelectFolderAsync();
}