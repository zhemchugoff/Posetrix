﻿using Posetrix.Core.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace Posetrix.Core.Services;
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Method <c>AddCommonServices</c> registers shared ViewModels.
    /// </summary>
    public static void AddCommonServices(this IServiceCollection collection)
    {
        collection.AddSingleton<MainViewModel>();
        collection.AddSingleton<SettingsViewModel>();
        collection.AddSingleton<CustomIntervalViewModel>();
        collection.AddSingleton<PredefinedIntervalsViewModel>();
        //collection.AddSingleton<ModelFactory>();

        collection.AddTransient<FolderAddViewModel>();
        collection.AddTransient<SessionViewModel>();

    }
}