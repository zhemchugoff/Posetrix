using Microsoft.Extensions.DependencyInjection;
using Posetrix.Avalonia.Views;
using Posetrix.Core.Interfaces;

namespace Posetrix.Avalonia.Services;

public static class ConfigureServices
{
    public static void AddAvaloniaServices(this IServiceCollection collection)
    {
        // Views.
        collection.AddTransient<MainView>();
        collection.AddTransient<FoldersAddView>();
        collection.AddTransient<SettingsView>();
        collection.AddTransient<CustomIntervalView>();
        collection.AddTransient<PredefinedIntervalsView>();
        collection.AddTransient<SessionView>();

        // Services.
        collection.AddTransient<IFolderBrowserServiceAsync, FolderBrowserService>();
        collection.AddTransient<IExtensionsService, SupportedExtensionsService>();
        collection.AddTransient<IUserSettings, UserSettings>();
        collection.AddTransient<IThemeService, ThemeService>();
        collection.AddTransient<ISoundService, SoundService>();
        collection.AddTransient<IImageResolutionService, ImageResolutionService>();
    }
}