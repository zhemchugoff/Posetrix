using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Avalonia.Services;
using Posetrix.Avalonia.Views;
using Posetrix.Core.Factories;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Services;
using Posetrix.Core.ViewModels;
using System;
using System.Diagnostics;
using System.Linq;


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
            var services = new ServiceCollection();
            
            services.AddCommonServices();
            services.AddAvaloniaServices();
            
            services.AddSingleton<ViewModelLocator>();
            services.AddSingleton<ServiceLocator>();
            services.AddSingleton<IWindowManager, WindowManager>();
            services.AddSingleton<WindowMapper>();

            // _services.AddTransient<IExtensionsService, SupportedExtensionsService>();
            // _services.AddTransient<IContentService, PlaceHolderImageService>();

            services.AddTransient<IFolderBrowserServiceAsync>(sp =>
                new FolderBrowserService(sp.GetRequiredService<FoldersAddView>));

            // _services.AddSingleton<ModelFactory>();
            


            // Creates a ServiceProvider containing services from the provided IServiceCollection.
            ServiceProvider = services.BuildServiceProvider();

            desktop.MainWindow = ServiceProvider.GetRequiredService<MainView>();
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