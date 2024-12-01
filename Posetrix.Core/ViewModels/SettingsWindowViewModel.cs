using System.Collections.ObjectModel;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;

namespace Posetrix.Core.ViewModels;

public partial class SettingsWindowViewModel : BaseViewModel, ICustomWindow
{
    public string WindowTitle => "Settings";
    public Theme SelectedTheme { get; set; }

    public ObservableCollection<Theme> Themes { get; } =
    [
        new() { Name = "System" },
        new() { Name = "Light" },
        new() { Name = "Dark" }
    ];

    public SettingsWindowViewModel()
    {
        SelectedTheme = Themes.First();
    }
}