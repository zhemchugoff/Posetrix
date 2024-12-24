using Posetrix.Core.Interfaces;

namespace Posetrix.Avalonia.Services;

public class UserSettings: IUserSettings
{
    public string Theme { get; set; }
    public string Sound { get; set; }
    public string ImageResolution { get; set; }
    public void Save()
    {
        throw new System.NotImplementedException();
    }
}