using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.Interfaces;

namespace Posetrix.Core.Services;

/// <summary>
/// Class <c>ViewModelLocator</c> gets required services from <c>IServiceProvider</c>.
/// </summary>
public class ServiceLocator(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    public IFolderBrowserServiceAsync FolderBrowserService => _serviceProvider.GetRequiredService<IFolderBrowserServiceAsync>();
    public IExtensionsService ExtensionsService => _serviceProvider.GetRequiredService<IExtensionsService>();
    public IUserSettings UserSettings => _serviceProvider.GetRequiredService<IUserSettings>();
    public IThemeService ThemeService => _serviceProvider.GetRequiredService<IThemeService>();
    public ISoundService SoundService => _serviceProvider.GetRequiredService<ISoundService>();
    public IImageResolutionService ImageResolutionService => _serviceProvider.GetRequiredService<IImageResolutionService>();

}
