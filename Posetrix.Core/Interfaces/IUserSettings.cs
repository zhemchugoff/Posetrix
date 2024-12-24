namespace Posetrix.Core.Interfaces;

public interface IUserSettings
{
    string Theme { get; set; }
    string Sound { get; set; }
    string ImageResolution { get; set; }
    void Save();
}
