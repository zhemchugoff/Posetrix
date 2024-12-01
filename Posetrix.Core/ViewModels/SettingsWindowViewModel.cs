using System.Collections.ObjectModel;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;

namespace Posetrix.Core.ViewModels;

public partial class SettingsWindowViewModel : BaseViewModel, ICustomWindow
{

    public string WindowTitle => "Settings";

    public ObservableCollection<Theme> Themes { get; set; } =
    [
        new() { Name = "System" },
        new() { Name = "Light" },
        new() { Name = "Dark" }
    ];
    public Theme SelectedTheme { get; set; }
    public SettingsWindowViewModel()
    {
        SelectedTheme = Themes.First();
    }
}
