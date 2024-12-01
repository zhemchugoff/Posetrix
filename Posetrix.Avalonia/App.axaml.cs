using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Avalonia.Controls;
using Posetrix.Avalonia.Services;
using Posetrix.Avalonia.Views;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Services;
using Posetrix.Core.ViewModels;

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
        
        // Register all the services needed for the application to run.
        var collection = new ServiceCollection();
        collection.AddCommonServices();
        
        // Add shared services.
        collection.AddTransient<MainWindow>();
        collection.AddTransient<IFolderBrowserService, FolderBrowserService>();
        collection.AddTransient<FolderAddWindow>();
        collection.AddTransient<SettingsWindow>();
        collection.AddTransient<CustomIntervalControl>();
        collection.AddTransient<SessionWindow>();
        collection.AddTransient<PredefinedIntervalsControl>();
        
        // Creates a ServiceProvider containing services from the provided IServiceCollection.
        var services = collection.BuildServiceProvider();
        ServiceProvider = services;
        
        var vm = services.GetRequiredService<MainWindowViewModel>();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            
            desktop.MainWindow = new MainWindow()
            {
                DataContext = vm
            };
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