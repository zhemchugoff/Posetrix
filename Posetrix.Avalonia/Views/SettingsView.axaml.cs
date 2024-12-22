using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using Posetrix.Core.Models;
using Posetrix.Core.Services;
using System.IO;

namespace Posetrix.Avalonia.Views;

public partial class SettingsView : Window
{
    public SettingsView()
    {
        InitializeComponent();
        using Stream stream = Assets.ResourceHelper.GetEmbeddedResourceStream(EmbeddedResourceLocator.WindowIcon);
        Icon = new WindowIcon(stream);
    }

    private void ChangeTheme_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ChangeTheme.SelectedItem is Theme theme && Application.Current != null)
        {
            var selectedValue = theme.Name;

            Application.Current.RequestedThemeVariant = selectedValue switch
            {
                "Dark" => ThemeVariant.Dark,
                "Light" => ThemeVariant.Light,
                "System" => ThemeVariant.Default,
                _ => Application.Current.RequestedThemeVariant
            };
        }
    }
}