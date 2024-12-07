using Posetrix.Core.Data;
using Posetrix.Core.ViewModels;

namespace Posetrix.Core.Factories;

public class WindowFactory(Func<ApplicationWindowNames, DynamicViewModel> factory)
{
    public DynamicViewModel GetViewModelName(ApplicationWindowNames windowName) => factory.Invoke(windowName);
}