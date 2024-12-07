using System.Collections.ObjectModel;
using Posetrix.Core.Data;
using Posetrix.Core.Factories;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;

namespace Posetrix.Core.ViewModels;

public class SettingsViewModel : DynamicWindowViewModel, ICustomWindow
{
    
    public string WindowTitle => "Settings";
    public Theme SelectedTheme { get; set; }

    public ObservableCollection<Theme> Themes { get; } =
    [
        new() { Name = "System" },
        new() { Name = "Light" },
        new() { Name = "Dark" }
    ];

    public SettingsViewModel()
    {
        WindowName = ApplicationWindowNames.Settings;
        SelectedTheme = Themes.First();
    }
}