using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Avalonia.Services;
using Posetrix.Avalonia.Views;
using Posetrix.Core.Data;
using Posetrix.Core.Factories;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Services;
using Posetrix.Core.ViewModels;
using System;
using System.Linq;
using CustomIntervalView = Posetrix.Avalonia.Views.CustomIntervalView;
using PredefinedIntervalsView = Posetrix.Avalonia.Views.PredefinedIntervalsView;


namespace Posetrix.Avalonia;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Without this line you will get duplicate validations from both Avalonia and CT.
        BindingPlugins.DataValidators.RemoveAt(0);


        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();

            // Register all the services needed for the application to run.
            var collection = new ServiceCollection();

            // Add viewodels.
            collection.AddCommonServices();

            // Add windows.
            collection.AddSingleton<MainView>();
            collection.AddTransient<FolderAddView>();
            collection.AddTransient<SettingsView>();
            collection.AddTransient<SessionView>();

            // Add custom controls.
            collection.AddTransient<CustomIntervalView>();
            collection.AddTransient<PredefinedIntervalsView>();

            // Add services.

            collection.AddTransient<IExtensionsService, SupportedExtensionsService>();
            collection.AddTransient<IContentService, PlaceHolderImageService>();

            collection.AddTransient<IFolderBrowserServiceAsync>(sp =>
                new FolderBrowserService(sp.GetRequiredService<FolderAddView>));


            collection.AddSingleton<Func<ApplicationModelNames, DynamicViewModel>>(s => name => name switch
            {
                ApplicationModelNames.CustomInterval => s.GetRequiredService<CustomIntervalViewModel>(),
                ApplicationModelNames.PredefinedIntervals => s.GetRequiredService<PredefinedIntervalsViewModel>(),
                _ => throw new InvalidOperationException()
            });

            collection.AddSingleton<ModelFactory>();

            // Creates a ServiceProvider containing services from the provided IServiceCollection.
            ServiceProvider = collection.BuildServiceProvider();

            desktop.MainWindow = ServiceProvider.GetRequiredService<MainView>();
            ;
            desktop.MainWindow.DataContext = ServiceProvider.GetRequiredService<MainViewModel>();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove.
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found.
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}