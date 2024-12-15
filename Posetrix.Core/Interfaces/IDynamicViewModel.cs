namespace Posetrix.Core.Interfaces;

public interface IDynamicViewModel
{
    string DisplayName { get; }
    bool CanStart { get; }
    int GetSeconds();
}
