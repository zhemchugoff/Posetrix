using Posetrix.Core.ViewModels;

namespace Posetrix.Core.Models;

public abstract class DynamicViewModel: BaseViewModel
{
    public abstract string DisplayName { get; }
    public abstract int GetSeconds();
}
