using Posetrix.Core.Data;
using Posetrix.Core.ViewModels;

namespace Posetrix.Core.Factories;

public class ModelFactory(Func<ApplicationModelNames, DynamicViewModel> factory)
{
    public DynamicViewModel GetViewModelName(ApplicationModelNames modelName) => factory.Invoke(modelName);
}