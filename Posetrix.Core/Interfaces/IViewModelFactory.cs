using Posetrix.Core.Enums;
using Posetrix.Core.ViewModels;

namespace Posetrix.Core.Interfaces;

public interface IViewModelFactory
{
    BaseViewModel GetViewModel(ViewModelNames name);
}