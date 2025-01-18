namespace Posetrix.Core.Interfaces;

public interface ISharedSessionParametersService
{
    int? Seconds { get; set; }
    int? SessionImageCount { get; set; }
    bool IsShuffleEnabled { get; set; }
    bool IsEndlessModeEnabled { get; set; }
}
