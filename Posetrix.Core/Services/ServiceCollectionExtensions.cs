using Posetrix.Avalonia.Services;
using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;

namespace Posetrix.Core.Services;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        // Main application window.
        // collection.AddTransient<MainWindow>();
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddTransient<IConfigService, ConfigService>();
        collection.AddTransient<IContentService, ContentService>();

        // Windows with add references button and settings button.
        collection.AddTransient<IFolderBrowserService, FolderBrowserService>();
        // collection.AddTransient<FoldersAddWindow>();

        // collection.AddTransient<SettingsWindow>();
        collection.AddSingleton<SettingsWindowViewModel>();


        // collection.AddTransient<CustomInterval>();
        collection.AddSingleton<CustomIntervalViewModel>();
        // collection.AddTransient<PredefinedIntervals>();
        collection.AddSingleton<PredefinedIntervalsViewModel>();

        // collection.AddTransient<SessionWindow>();
        collection.AddTransient<SessionWindowViewModel>();
    }
}