using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.Data;
using Posetrix.Core.Factories;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Services;
using Posetrix.Core.ViewModels;
using Posetrix.Services;
using Posetrix.Views;

namespace Posetrix
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Configure services.
            var serviceCollection = new ServiceCollection();

            // Add viewmodels.
            serviceCollection.AddCommonServices();
            
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainView>();
            mainWindow.DataContext = ServiceProvider.GetRequiredService<MainViewModel>();
            mainWindow.Show();
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            // Add windows.
            serviceCollection.AddTransient<MainView>();
            serviceCollection.AddTransient<FoldersAddView>();
            serviceCollection.AddTransient<SettingsView>();
            serviceCollection.AddTransient<CustomIntervalView>();
            serviceCollection.AddTransient<PredefinedIntervalsView>();
            serviceCollection.AddTransient<SessionView>();
            
            serviceCollection.AddSingleton<Func<ApplicationModelNames, DynamicViewModel>>(s => name => name switch
            {
                ApplicationModelNames.CustomInterval => s.GetRequiredService<CustomIntervalViewModel>(),
                ApplicationModelNames.PredefinedIntervals => s.GetRequiredService<PredefinedIntervalsViewModel>(),
                _ => throw new InvalidOperationException()
            } );

            serviceCollection.AddSingleton<ModelFactory>();

            // Add services.
            serviceCollection.AddTransient<IFolderBrowserServiceAsync, FolderBrowserService>();
            serviceCollection.AddTransient<IExtensionsService, SupportedExtensionsService>();
            //serviceCollection.AddTransient<IContentService, PlaceHolderImage>();
        }
    }
}