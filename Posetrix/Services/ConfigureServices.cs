using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.Data;
using Posetrix.Core.Factories;
using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;
using Posetrix.Views;

namespace Posetrix.Services;

public static class ConfigureServices
{
    public static void AddWPFServices(this IServiceCollection collection)
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
    }
}
