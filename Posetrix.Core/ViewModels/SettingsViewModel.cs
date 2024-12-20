using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using Posetrix.Core.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Posetrix.Core.ViewModels;

public partial class SettingsViewModel : BaseViewModel, ICustomWindow
{
    private readonly IUserSettings _userSettings;

    public string WindowTitle => "Settings";
    [ObservableProperty] public partial string SelectedTheme { get; set; }

    private readonly IThemeService _themeService;

    public string Theme
    {
        get => _userSettings.Theme;
        set
        {
            _userSettings.Theme = value;
            _userSettings.Save();
        }
    }
    public string Sound
    {
        get => _userSettings.Sound;
        set
        {
            _userSettings.Sound = value;
            _userSettings.Save();
        }
    }

    public ObservableCollection<string> Themes { get; } = ["System", "Light", "Dark"];

    public SettingsViewModel(ServiceLocator serviceLocator)
    {
        _userSettings = serviceLocator.UserSettings;
        _themeService = serviceLocator.ThemeService;

        SelectedTheme = Theme;
    }

    [RelayCommand]
    private void SaveSettings()
    {
        _userSettings.Theme = Theme;
        _userSettings.Sound = Sound;
    }

    partial void OnSelectedThemeChanged(string value)
    {
        Theme = value;
        _themeService.SetTheme(Theme);
    }
}