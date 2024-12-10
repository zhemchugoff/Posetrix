using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Services;
using Posetrix.Core.ViewModels;
using Posetrix.Services;
using Posetrix.Views;

namespace Posetrix
{

    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        private readonly ServiceCollection services = new();

        public App()
        {
            // Configure services.
            services.AddCommonServices();
            services.AddWPFServices();

            services.AddSingleton<ViewModelLocator>();
            services.AddSingleton<WindowMapper>();
            services.AddSingleton<IWindowManager, WindowManager>();

            ServiceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Main window.
            var windowManager = ServiceProvider.GetRequiredService<IWindowManager>();
            windowManager.ShowWindow(ServiceProvider.GetRequiredService<MainViewModel>());
        }
    }
}