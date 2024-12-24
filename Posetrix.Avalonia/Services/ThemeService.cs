using Avalonia;
using Avalonia.Styling;
using Posetrix.Core.Interfaces;

namespace Posetrix.Avalonia.Services;

public class ThemeService : IThemeService
{
    public void SetTheme(string theme)
    {
        if (Application.Current != null)
        {
            Application.Current.RequestedThemeVariant = theme switch
            {
                "Dark" => ThemeVariant.Dark,
                "Light" => ThemeVariant.Light,
                "System" => ThemeVariant.Default,
                _ => Application.Current.RequestedThemeVariant
            };
        }
    }
}