using Posetrix.Core.Interfaces;
using System.IO;
using System.Text.Json;

namespace Posetrix.Services;

public class UserSettings : IUserSettings
{
    private readonly string _settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Posetrix.json");
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

    private class SettingsData
    {
        public string Theme { get; set; } = "System";
        public string Sound { get; set; } = "Classic Countdown";
    }

    private SettingsData _settings = new();

    public string Theme
    {
        get => _settings.Theme;
        set => _settings.Theme = value;
    }
    public string Sound
    {
        get => _settings.Sound;
        set => _settings.Sound = value;
    }

    public UserSettings()
    {
        Load();
    }

    private void Load()
    {
        if (File.Exists(_settingsFilePath))
        {
            var json = File.ReadAllText(_settingsFilePath);
            _settings = JsonSerializer.Deserialize<SettingsData>(json) ?? new SettingsData();
        }
        else
        {
            _settings = new SettingsData();
            Save(); // Create a new settings file if it doesn't exist.
        }
    }

    public void Save()
    {
        string json = JsonSerializer.Serialize(_settings, _jsonSerializerOptions);
        File.WriteAllText(_settingsFilePath, json);
    }
}
