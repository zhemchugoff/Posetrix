using NSubstitute;
using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;

namespace Posetrix.Tests;

public class SettingsViewModelTests
{
    private readonly IUserSettings _mockUserSettings;
    private readonly IThemeService _mockThemeService;
    private readonly ISoundService _mockSoundService;
    private readonly IRuntimeInformation _mockRuntimeInformation;

    public SettingsViewModelTests()
    {
        _mockUserSettings = Substitute.For<IUserSettings>();
        _mockThemeService = Substitute.For<IThemeService>();
        _mockSoundService = Substitute.For<ISoundService>();
        _mockRuntimeInformation = Substitute.For<IRuntimeInformation>();
    }
    [Fact]
    public void WindowTitle_ShouldNotBeEmpty()
    {
        // Arrange.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        // Act.
        string actualWindowTitle = settingsViewModel.WindowTitle;
        // Assert.
        Assert.NotEmpty(actualWindowTitle);
    }
    [Fact]
    public void WindowTitle_ShouldHaveConstValue()
    {
        // Arrange.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        string expectedWindowTitle = "Settings";
        // Act.
        string actualWindowTitle = settingsViewModel.WindowTitle;
        // Assert.
        Assert.Equal(expectedWindowTitle, actualWindowTitle);
    }
    [Fact]
    public void SourceLink_ShouldNotBeEmpty()
    {
        // Arrange.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        // Act.
        string actualSourceLink = settingsViewModel.SourceLink;
        // Assert.
        Assert.NotEmpty(actualSourceLink);
    }
    [Fact]
    public void SourceLink_ShouldHaveConstValue()
    {
        // Arrange.
        string expectedSourceLink = "Source code: https://github.com/zhemchugoff/Posetrix";
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        // Act.
        string actualSourceLink = settingsViewModel.SourceLink;
        // Assert.
        Assert.Equal(expectedSourceLink, actualSourceLink);
    }

    [Fact]
    public void IsSoundEnabled_WhenSelectedSoundIsOff_ShouldReturnFalse()
    {
        // Arrange.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation)
        {
            SelectedSound = "Off"
        };
        // Act.
        bool actualIsSoundEnabled = settingsViewModel.IsSoundEnabled;
        // Assert.
        Assert.False(actualIsSoundEnabled);
    }

    [Theory]
    [InlineData("Classic Countdown")]
    [InlineData("Beep Countdown")]
    [InlineData("Three Two One Countdown")]
    public void IsSoundEnabled_WhenSelectedSoundIsNotOff_ShouldReturnTrue(string selectedSound)
    {
        // Arrange.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation)
        {
            SelectedSound = selectedSound
        };
        // Act.
        bool actualIsSoundEnabled = settingsViewModel.IsSoundEnabled;
        // Assert.
        Assert.True(actualIsSoundEnabled);
    }

    [Fact]
    public void Themes_ShouldHaveThreeItems()
    {
        // Arrange.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        // Act.
        int actualThemesCount = settingsViewModel.Themes.Count;
        // Assert.
        Assert.Equal(3, actualThemesCount);
    }

    [Fact]
    public void Themes_ShouldHaveThreeSpecificItems()
    {
        // Arrange.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        List<string> expectedThemes = ["System", "Light", "Dark"];
        // Act.
        List<string> actualThemes = settingsViewModel.Themes;
        // Assert.
        Assert.NotEmpty(actualThemes);
        Assert.Equal(expectedThemes, actualThemes);
    }

    [Fact]
    public void Sounds_ShouldHaveFourItems()
    {
        // Arrange.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        // Act.
        int actualSoundsCount = settingsViewModel.Sounds.Count;
        // Assert.
        Assert.Equal(4, actualSoundsCount);
    }

    [Fact]
    public void Sounds_ShouldHaveFourSpecificItems()
    {
        // Arrange.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        List<string> expectedSounds = ["Off", "Classic Countdown", "Beep Countdown", "Three Two One Countdown"];
        // Act.
        List<string> actualSounds = settingsViewModel.Sounds;
        // Assert.
        Assert.Equal(expectedSounds, actualSounds);
    }

    [Fact]
    public void Resolutions_ShouldHaveFourItems()
    {
        // Arrange.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        // Act.
        int actualResolutionsCount = settingsViewModel.Resolutions.Count;
        // Assert.
        Assert.Equal(4, actualResolutionsCount);
    }

    [Fact]
    public void Resolutions_ShouldHaveFourSpecificItems()
    {
        // Arrange.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        List<string> expectedResolutions = ["Default", "Low", "Medium", "High"];
        // Act.
        List<string> actualResolutions = settingsViewModel.Resolutions;
        // Assert.
        Assert.Equal(expectedResolutions, actualResolutions);
    }

    [Fact]
    public void RuntimeInformation_ShouldHaveStringValue_WhenConstructed()
    {
        // Arrange.
        string runtimeInformation = "9.0.0";
        _mockRuntimeInformation.NetVersion.Returns(runtimeInformation);
        string expectedRuntimeInformation = $".NET version: {runtimeInformation}";
        // Act.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        string actualRuntimeInformation = settingsViewModel.RuntimeInformation;
        // Assert.
        Assert.Equal(expectedRuntimeInformation, actualRuntimeInformation);
    }

    [Fact]
    public void Theme_ShouldReturnDefaultThemeValue()
    {
        // Arrange.
        string expectedTheme = "System";
        _mockUserSettings.Theme.Returns(expectedTheme);
        // Act.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        string actualTheme = settingsViewModel.Theme;
        // Assert.
        Assert.Equal(expectedTheme, actualTheme);
    }

    [Fact]
    public void Theme_ShouldChangeValue_WhenGivenString()
    {
        // Arrange.
        string defaultTheme = "System";
        _mockUserSettings.Theme.Returns(defaultTheme);
        string expectedTheme = "Light";
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation)
        {
            // Act.
            Theme = expectedTheme
        };
        string actualTheme = settingsViewModel.Theme;
        // Assert.
        Assert.Equal(expectedTheme, actualTheme);
    }

    [Fact]
    public void Sound_ShouldReturnDefaultSoundValue()
    {
        // Arrange.
        string expectedSound = "Off";
        _mockUserSettings.Sound.Returns(expectedSound);
        // Act.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        string actualSound = settingsViewModel.Sound;
        // Assert.
        Assert.Equal(expectedSound, actualSound);
    }

    [Fact]
    public void Sound_ShouldChangeValue_WhenGivenString()
    {
        // Arrange.
        string defaultSound = "Off";
        _mockUserSettings.Sound.Returns(defaultSound);
        string expectedSound = "Classic Countdown";
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation)
        {
            // Act.
            Sound = expectedSound
        };
        string actualSound = settingsViewModel.Sound;
        // Assert.
        Assert.Equal(expectedSound, actualSound);
    }

    [Fact]
    public void ImageResolution_ShouldReturnDefaultImageResolution()
    {
        // Arrange.
        string expectedImageResolution = "High";
        _mockUserSettings.ImageResolution.Returns(expectedImageResolution);
        // Act.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        string actualImageResolution = settingsViewModel.ImageResolution;
        // Assert.
        Assert.Equal(expectedImageResolution, actualImageResolution);
    }

    [Fact]
    public void ImageResolution_ShouldChangeValue_WhenGivenString()
    {
        // Arrange.
        string defaultImageResolution = "High";
        _mockUserSettings.ImageResolution.Returns(defaultImageResolution);
        string expectedImageResolution = "Low";
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation)
        {
            // Act.
            ImageResolution = expectedImageResolution
        };
        string actualImageResolution = settingsViewModel.ImageResolution;
        // Assert.
        Assert.Equal(expectedImageResolution, actualImageResolution);
    }

    [Fact]
    public void SelectedTheme_ShouldReturnDefaultValue()
    {
        // Arrange.
        string expectedTheme = "System";
        _mockUserSettings.Theme.Returns(expectedTheme);
        // Act.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        string actualTheme = settingsViewModel.SelectedTheme;
        // Assert.
        Assert.Equal(expectedTheme, actualTheme);
    }

    [Fact]
    public void SelectedTheme_ShouldSetAndNotifyPropertyChanged()
    {
        // Arrange.
        string defaultTheme = "System";
        _mockUserSettings.Theme.Returns(defaultTheme);
        var propertyChangedCount = 0;
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        settingsViewModel.PropertyChanged += (sender, args) => propertyChangedCount++;
        // Act.
        settingsViewModel.SelectedTheme = "Light";
        settingsViewModel.SelectedTheme = "Dark";
        settingsViewModel.SelectedTheme = "System";
        // Assert.
        Assert.Equal(3, propertyChangedCount);
    }

    [Fact]
    public void SelectedTheme_ShouldSetUserSettingsTheme_WhenValueChanged()
    {
        // Arrange.
        string defaultSelectedTheme = "System";
        _mockUserSettings.Sound.Returns(defaultSelectedTheme);
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation)
        {
            // Act.
            SelectedTheme = "Light"
        };
        // Assert.
        _mockUserSettings.Received().Theme = "Light";
    }

    [Fact]
    public void SelectedImageResolution_ShouldReturnDefaultValue()
    {
        // Arrange.
        string expectedImageResolution = "High";
        _mockUserSettings.ImageResolution.Returns(expectedImageResolution);
        // Act.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        string actualImageResolution = settingsViewModel.SelectedImageResolution;
        // Assert.
        Assert.Equal(expectedImageResolution, actualImageResolution);
    }

    [Fact]
    public void ImageResolution_ShouldSetAndNotifyPropertyChanged()
    {
        // Arrange.
        string defaultImageResolution = "High";
        _mockUserSettings.ImageResolution.Returns(defaultImageResolution);
        var propertyChangedCount = 0;
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        settingsViewModel.PropertyChanged += (sender, args) => propertyChangedCount++;
        // Act.
        settingsViewModel.SelectedImageResolution = "Low";
        settingsViewModel.SelectedImageResolution = "Medium";
        settingsViewModel.SelectedImageResolution = "Low";
        settingsViewModel.SelectedImageResolution = "Default";
        // Assert.
        Assert.Equal(4, propertyChangedCount);
    }

    [Fact]
    public void SelectedImageResolution_ShouldSetUserSettingsResolution_WhenValueChanged()
    {
        // Arrange.
        string defaultImageResolution = "Off";
        _mockUserSettings.Sound.Returns(defaultImageResolution);
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation)
        {
            // Act.
            SelectedImageResolution = "Low"
        };
        // Assert.
        _mockUserSettings.Received().ImageResolution = "Low";
    }

    [Fact]
    public void SelectedSound_ShouldReturnDefaultValue()
    {
        // Arrange.
        string expectedSelectedSound = "Off";
        _mockUserSettings.Sound.Returns(expectedSelectedSound);
        // Act.
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        string actualSelectedSound = settingsViewModel.SelectedSound;
        // Assert.
        Assert.Equal(expectedSelectedSound, actualSelectedSound);
    }

    [Fact]
    public void IsSoundEnabled_ShouldReturnTrue_WhenSelectedSoundIsNotOff()
    {
        // Arrange.
        string defaultSelectedSound = "Off";
        _mockUserSettings.Sound.Returns(defaultSelectedSound);
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        // Act.
        Assert.False(settingsViewModel.IsSoundEnabled);
        settingsViewModel.SelectedSound = "Classic Countdown";
        // Assert.
        Assert.True(settingsViewModel.IsSoundEnabled);

    }

    [Fact]
    public void SelectedSound_ShouldSetAndNotifyPropertyChanged()
    {
        // Arrange.
        string defaultSelectedSound = "Off";
        _mockUserSettings.Sound.Returns(defaultSelectedSound);
        var propertyChangedCount = 0;
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        settingsViewModel.PropertyChanged += (sender, args) => propertyChangedCount++;
        // Act.
        settingsViewModel.SelectedSound = "Classic Countdown";
        settingsViewModel.SelectedSound = "Beep Countdown";
        settingsViewModel.SelectedSound = "Classic Countdown";
        // Assert.
        Assert.Equal(6, propertyChangedCount); // Also notifies IsSoundEnabled property.
    }

    [Fact]
    public void SelectedSound_ShouldSetUserSettingsSound_WhenValueChanged()
    {
        // Arrange.
        string defaultSelectedSound = "Off";
        _mockUserSettings.Sound.Returns(defaultSelectedSound);
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation)
        {
            // Act.
            SelectedSound = "Classic Countdown"
        };
        // Assert.
        _mockUserSettings.Received().Sound = "Classic Countdown";
    }

    [Fact]
    public async Task PlaySound_ShouldNotPlaySound_WhenSelectedSoundIsOff()
    {
        // Arrange.
        string defaultSelectedSound = "Off";
        _mockUserSettings.Sound.Returns(defaultSelectedSound);
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation);
        // Act.
        if (settingsViewModel.PlaySoundCommand.CanExecute(null))
        {
            settingsViewModel.PlaySoundCommand.Execute(null);
        }
        // Assert.
        Assert.False(settingsViewModel.PlaySoundCommand.CanExecute(null));
        await _mockSoundService.DidNotReceive().PlaySound("Off");
    }

    [Fact]
    public async Task PlaySound_ShouldPlaySoundOnce_WhenSelectedSoundIsNotOff()
    {
        // Arrange.
        string defaultSelectedSound = "Off";
        _mockUserSettings.Sound.Returns(defaultSelectedSound);
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation)
        {
            // Act.
            SelectedSound = "Classic Countdown"
        };

        if (settingsViewModel.PlaySoundCommand.CanExecute(null))
        {
            settingsViewModel.PlaySoundCommand.Execute(null);
        }
        // Assert.
        Assert.True(settingsViewModel.PlaySoundCommand.CanExecute(null));
        await _mockSoundService.Received(1).PlaySound("Classic Countdown");
    }

    [Fact]
    public async Task PlaySound_ShouldPlaySoundTwice_WhenSelectedSoundIsNotOff()
    {
        // Arrange.
        string defaultSelectedSound = "Off";
        _mockUserSettings.Sound.Returns(defaultSelectedSound);
        var settingsViewModel = new SettingsViewModel(_mockUserSettings, _mockThemeService, _mockSoundService, _mockRuntimeInformation)
        {
            // Act.
            SelectedSound = "Classic Countdown"
        };

        if (settingsViewModel.PlaySoundCommand.CanExecute(null))
        {
            settingsViewModel.PlaySoundCommand.Execute(null);
            settingsViewModel.PlaySoundCommand.Execute(null);
        }
        // Assert.
        Assert.True(settingsViewModel.PlaySoundCommand.CanExecute(null));
        await _mockSoundService.Received(2).PlaySound("Classic Countdown");
    }
}
