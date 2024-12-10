using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;

namespace Posetrix.Core.Services;

using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.Data;
using Posetrix.Core.Factories;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Method <c>AddCommonServices</c> registers shared ViewModels.
    /// </summary>
    public static void AddCommonServices(this IServiceCollection collection)
    {
        collection.AddSingleton<MainViewModel>();
        collection.AddSingleton<SettingsViewModel>();
        //collection.AddSingleton<ModelFactory>();

        collection.AddTransient<CustomIntervalViewModel>();
        collection.AddTransient<PredefinedIntervalsViewModel>();
        collection.AddTransient<SessionViewModel>();

    }
}