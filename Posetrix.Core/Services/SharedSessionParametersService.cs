using Posetrix.Core.Interfaces;

namespace Posetrix.Core.Services;

public class SharedSessionParametersService : ISharedSessionParametersService
{
    public int? Seconds { get; set; } = 0;
    public int? SessionImageCount { get; set; } = 0;
    public bool IsShuffleEnabled { get; set; } = false;
    public bool IsEndlessModeEnabled { get; set; } = false;
}
