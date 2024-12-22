using System.Reflection;

namespace Posetrix.Assets;

public static class ResourceHelper
{
    /// <summary>
    /// Method <c>GetEmbeddedResourceStream</c> gets asset file path in format Folder.FileName.extension and returns file stream.
    /// </summary>
    public static Stream GetEmbeddedResourceStream(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceFullName = $"Posetrix.Assets.{resourceName}";
        var stream = assembly.GetManifestResourceStream(resourceFullName);
        if (stream == null) { throw new Exception($"Resource '{resourceFullName}' not found."); }
        return stream;
    }
}
