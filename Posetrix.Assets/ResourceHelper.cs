using System.Reflection;

namespace Posetrix.Assets
{
    public static class ResourceHelper
    {
        public static Stream GetEmbeddedResourceStream(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            // var resourceFullName = $"Posetrix.Assets.Images.{resourceName}";
            var resourceFullName = $"Posetrix.Assets.{resourceName}";
            var stream = assembly.GetManifestResourceStream(resourceFullName);
            if (stream == null) { throw new Exception($"Images '{resourceFullName}' not found."); }
            return stream;
        }
    }
}
