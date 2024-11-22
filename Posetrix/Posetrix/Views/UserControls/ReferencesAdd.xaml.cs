using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.ViewModels;
using System.Windows.Controls;

namespace Posetrix.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ReferencesAdd.xaml
    /// </summary>
    public partial class ReferencesAdd : UserControl
    {
        private readonly IServiceProvider _serviceProvider;

        //private readonly SettingsWindow _settingsWindow;

        public ReferencesAdd(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this._serviceProvider = serviceProvider;


            //_settingsWindow = settingsWindow;
        }

        private void ShowSettingsWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var settingsWindow = _serviceProvider.GetRequiredService<SettingsWindow>();
            settingsWindow.Show();  
        }

        private void AddReferencesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var foldersAddWindow = _serviceProvider.GetRequiredService<FoldersAddWindow>();
            foldersAddWindow.Show();
        }
    }
}
