using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;
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

            // Configure services
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            // Main application window
            serviceCollection.AddTransient<MainWindow>();
            serviceCollection.AddTransient<MainWindowViewModel>();
            // Windows with add references button and settings button
            serviceCollection.AddTransient<ReferencesAdd>();
            serviceCollection.AddTransient<IFolderBrowserService, FolderBrowserService>();
            serviceCollection.AddTransient<FoldersAddWindow>();
            serviceCollection.AddSingleton<FoldersAddWindowViewModel>();

            serviceCollection.AddTransient<SettingsWindow>();
            serviceCollection.AddSingleton<SettingsWindowViewModel>();

            serviceCollection.AddTransient<PracticeModes>();
            serviceCollection.AddTransient<CustomInterval>();
            serviceCollection.AddTransient<CustomIntervalViewModel>();
            serviceCollection.AddTransient<PredefinedIntervals>();
            serviceCollection.AddTransient<PredefinedIntervalsViewModel>();
            serviceCollection.AddTransient<PracticeModesViewModel>();
        }
    }

}
