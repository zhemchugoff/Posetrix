using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.Interfaces;
using Posetrix.Views;

namespace Posetrix.Services;

public static class ConfigureWPFServices
{
    public static void AddWPFViews(this IServiceCollection collection)
    {
        collection.AddTransient<MainView>();
        collection.AddTransient<FoldersAddView>();
        collection.AddTransient<SettingsView>();
        collection.AddTransient<CustomIntervalView>();
        collection.AddTransient<PredefinedIntervalsView>();
        collection.AddTransient<SessionView>();
    }
    public static void AddWPFServices(this IServiceCollection collection)
    {
        collection.AddTransient<IFolderBrowserServiceAsync, FolderBrowserService>();
        collection.AddTransient<IExtensionsService, SupportedExtensionsService>();
        collection.AddTransient<IUserSettings, UserSettings>();
        collection.AddTransient<IThemeService, ThemeService>();
        collection.AddTransient<ISoundService, SoundService>();
        collection.AddTransient<IImageResolutionService, ImageResolutionService>();
        collection.AddTransient<IRuntimeInformation, RuntimeInformationService>();
    }
}
