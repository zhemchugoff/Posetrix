using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;

namespace Posetrix.Avalonia.Views;

public partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        InitializeComponent();
    }

    private void ChangeTheme_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0 && (ComboBoxItem)e.AddedItems[0] is not null)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)e.AddedItems[0];

            string selectedValue = selectedItem.Content.ToString();

            if (Application.Current == null) return;
            Application.Current.RequestedThemeVariant = selectedValue switch
            {
                "Light" => ThemeVariant.Light,
                "Dark" => ThemeVariant.Dark,
                "System" => ThemeVariant.Default,
                _ => Application.Current.RequestedThemeVariant
            };
        }
    }
}