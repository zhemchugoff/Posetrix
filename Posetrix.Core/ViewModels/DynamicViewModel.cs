using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Data;

namespace Posetrix.Core.ViewModels;

public partial class DynamicViewModel: BaseViewModel
{
    [ObservableProperty] private ApplicationModelNames _modelName;
}