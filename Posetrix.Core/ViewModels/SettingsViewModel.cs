using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Services;

namespace Posetrix.Core.ViewModels;

public partial class SettingsViewModel : BaseViewModel, ICustomWindow
{
    public string WindowTitle => "Settings";
    private readonly IUserSettings _userSettings;
    private readonly IThemeService _themeService;
    private readonly ISoundService _soundService;
    [ObservableProperty] public partial string SelectedTheme { get; set; }
    [ObservableProperty] public partial string SelectedImageResolution { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsSoundEnabled))]
    [NotifyCanExecuteChangedFor(nameof(PlaySoundCommand))]
    public partial string SelectedSound { get; set; }

    public bool IsSoundEnabled => SelectedSound != "Off";

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

    public string ImageResolution
    {
        get => _userSettings.ImageResolution;
        set
        {
            _userSettings.ImageResolution = value;
            _userSettings.Save();
        }
    }
    public List<string> Themes { get; } = ["System", "Light", "Dark"];
    public List<string> Sounds { get; } = ["Off", "Classic Countdown", "Beep Countdown", "Three Two One Countdown"];
    public List<string> Resolutions { get; } = ["Default", "Low", "Medium", "High"];

    public SettingsViewModel(ServiceLocator serviceLocator)
    {
        _userSettings = serviceLocator.UserSettings;
        _themeService = serviceLocator.ThemeService;
        _soundService = serviceLocator.SoundService;

        SelectedTheme = Theme;
        SelectedSound = Sound;
        SelectedImageResolution = ImageResolution;
    }

    partial void OnSelectedThemeChanged(string value)
    {
        Theme = value;
        _userSettings.Theme = Theme;
        _themeService.SetTheme(Theme);
    }

    partial void OnSelectedSoundChanged(string value)
    {
        Sound = value;
        _userSettings.Sound = Sound;
    }

    partial void OnSelectedImageResolutionChanged(string value)
    {
        ImageResolution = value;
        _userSettings.ImageResolution = ImageResolution;
    }

    [RelayCommand(CanExecute = nameof(IsSoundEnabled))]
    private void PlaySound()
    {
        _soundService.PlaySound(SelectedSound);
    }
}