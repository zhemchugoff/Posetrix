using Posetrix.Core.Enums;
using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;

namespace Posetrix.Core.Factories;

public class ViewModelFactory(Func<ViewModelNames, BaseViewModel> factory) : IViewModelFactory
{
    public BaseViewModel GetViewModel(ViewModelNames name) => factory.Invoke(name);
}
