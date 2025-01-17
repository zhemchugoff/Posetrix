using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Enums;
using Posetrix.Core.Interfaces;

namespace Posetrix.Core.ViewModels;

public partial class SettingsViewModel : BaseViewModel
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
    public string RuntimeInformation { get; private set; } = ".NET version: ";
    public string SourceLink => "Source code: https://github.com/zhemchugoff/Posetrix";

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

    public SettingsViewModel(IUserSettings userSettings, IThemeService themeService, ISoundService soundService, IRuntimeInformation runtimeInformation)
    {
        ViewModelName = ViewModelNames.Settings;
        _userSettings = userSettings;
        _themeService = themeService;
        _soundService = soundService;

        SelectedTheme = Theme;
        SelectedSound = Sound;
        SelectedImageResolution = ImageResolution;

        RuntimeInformation += runtimeInformation.NetVersion;
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
    private async Task PlaySound()
    {
        await _soundService.PlaySound(SelectedSound);
    }
}