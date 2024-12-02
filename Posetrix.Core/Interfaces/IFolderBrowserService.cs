namespace Posetrix.Core.Interfaces;
/// <summary>
/// An interface <c>IFolderBrowserService</c> for a dependency injection for using a new WPF Open Folder dialog.
/// </summary>
public interface IFolderBrowserService
{
    string? OpenFolderDialog();
}
