using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.Enums;
using Posetrix.Core.Factories;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Services;
using Posetrix.Core.ViewModels;
using Posetrix.Services;
using System.Windows;

namespace Posetrix;

public partial class App : Application
{
    private IServiceProvider ServiceProvider { get; set; }

    private readonly ServiceCollection collection = new();

    public App()
    {
        collection.AddViewModels();
        collection.AddWPFViews();
        collection.AddWPFServices();

        //collection.AddSingleton<ViewModelLocator>();
        collection.AddSingleton<WindowMapper>();
        collection.AddSingleton<IWindowManager, WindowManager>();

        collection.AddSingleton<Func<ViewModelNames, BaseViewModel>>(serviceProvider => name => name switch
        {
            ViewModelNames.Main => serviceProvider.GetRequiredService<MainViewModel>(),
            ViewModelNames.CustomInterval => serviceProvider.GetRequiredService<CustomIntervalViewModel>(),
            ViewModelNames.PredefinedIntervals => serviceProvider.GetRequiredService<PredefinedIntervalsViewModel>(),
            ViewModelNames.FolderAdd => serviceProvider.GetRequiredService<FolderAddViewModel>(),
            ViewModelNames.Settings => serviceProvider.GetRequiredService<SettingsViewModel>(),
            ViewModelNames.Session => serviceProvider.GetRequiredService<SessionViewModel>(),
            _ => throw new NotImplementedException()
        });

        collection.AddSingleton<IViewModelFactory, ViewModelFactory>();

        ServiceProvider = collection.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var windowManager = ServiceProvider.GetRequiredService<IWindowManager>();
        windowManager.ShowWindow(ServiceProvider.GetRequiredService<MainViewModel>());
    }
}