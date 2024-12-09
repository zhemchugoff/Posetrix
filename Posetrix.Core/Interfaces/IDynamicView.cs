using Posetrix.Core.Models;

namespace Posetrix.Core.Interfaces;

public interface IDynamicView
{
    string DisplayName { get; }
    SessionTimer SessionTimer { get; }
}
