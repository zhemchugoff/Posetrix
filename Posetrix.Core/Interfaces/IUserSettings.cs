namespace Posetrix.Core.Interfaces;

public interface IUserSettings
{
    string Theme { get; set; }
    string Sound { get; set; }

    void Save();
}
