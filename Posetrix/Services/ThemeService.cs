using Posetrix.Core.Interfaces;
using System.Windows;

namespace Posetrix.Services;

public class ThemeService : IThemeService
{
    public void SetTheme(string theme)
    {
        Application.Current.ThemeMode = theme switch
        {
            "Light" => ThemeMode.Light,
            "Dark" => ThemeMode.Dark,
            "System" => ThemeMode.System,
            _ => ThemeMode.System,
        };
    }
}
