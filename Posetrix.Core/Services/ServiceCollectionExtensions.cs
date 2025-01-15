using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.ViewModels;

namespace Posetrix.Core.Services;
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Method <c>AddCommonServices</c> registers shared ViewModels.
    /// </summary>
    public static void AddViewModels(this IServiceCollection collection)
    {
        collection.AddSingleton<MainViewModel>();

        collection.AddTransient<SettingsViewModel>();
        collection.AddTransient<CustomIntervalViewModel>();
        collection.AddTransient<PredefinedIntervalsViewModel>();
        collection.AddTransient<FolderAddViewModel>();
        collection.AddTransient<SessionViewModel>();
    }
}