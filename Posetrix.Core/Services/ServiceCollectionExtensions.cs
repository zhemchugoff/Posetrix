using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;

namespace Posetrix.Core.Services;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Method <c>AddCommonServices</c> registers app shared viewmodels.
    /// </summary>
    public static void AddCommonServices(this IServiceCollection collection)
    {
        // Main application window.
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddTransient<IConfigService, ConfigService>();
        collection.AddTransient<IContentService, ContentService>();
        collection.AddSingleton<SettingsWindowViewModel>();
        collection.AddSingleton<CustomIntervalViewModel>();
        collection.AddSingleton<PredefinedIntervalsViewModel>();
        collection.AddTransient<SessionWindowViewModel>();

        // Windows with add references button and settings button.
        // collection.AddTransient<MainWindow>();
        // collection.AddTransient<IFolderBrowserService, FolderBrowserService>();
        // collection.AddTransient<FoldersAddWindow>();
        // collection.AddTransient<SettingsWindow>();
        // collection.AddTransient<CustomInterval>();
        // collection.AddTransient<PredefinedIntervals>();

        // collection.AddTransient<SessionWindow>();
    }
}