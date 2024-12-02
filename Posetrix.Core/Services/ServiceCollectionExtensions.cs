using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;

namespace Posetrix.Core.Services;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Method <c>AddCommonServices</c> registers app shared ViewModels.
    /// </summary>
    public static void AddCommonServices(this IServiceCollection collection)
    {
        // Main application window.
        collection.AddSingleton<MainViewModel>();
        collection.AddTransient<IConfigService, ConfigService>();
        collection.AddTransient<IContentService, ContentService>();
        collection.AddSingleton<SettingsViewModel>();
        collection.AddSingleton<CustomIntervalViewModel>();
        collection.AddSingleton<PredefinedIntervalsViewModel>();
        collection.AddTransient<SessionViewModel>();
    }
}