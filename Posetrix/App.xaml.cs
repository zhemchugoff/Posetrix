using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Services;
using Posetrix.Services;
using Posetrix.Views;
using Posetrix.Views.UserControls;

namespace Posetrix
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider? ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Configure services.
            var serviceCollection = new ServiceCollection();

            // Add viewmodels.
            serviceCollection.AddCommonServices();

            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            // Add windows.
            serviceCollection.AddTransient<MainWindow>();
            serviceCollection.AddTransient<FoldersAddWindow>();
            serviceCollection.AddTransient<SettingsWindow>();
            serviceCollection.AddTransient<CustomInterval>();
            serviceCollection.AddTransient<PredefinedIntervals>();
            serviceCollection.AddTransient<SessionWindow>();

            // Add services.
            serviceCollection.AddTransient<IFolderBrowserServiceAsync, FolderBrowserService>();
            serviceCollection.AddTransient<IExtensionsService, SupportedExtensionsService>();
            serviceCollection.AddTransient<IContentService, PlaceHolderImageService>();
        }
    }
}