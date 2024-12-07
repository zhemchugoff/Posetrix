using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Data;

namespace Posetrix.Core.ViewModels;

public partial class DynamicWindowViewModel: BaseViewModel
{
    [ObservableProperty] private ApplicationWindowNames _windowName;
}