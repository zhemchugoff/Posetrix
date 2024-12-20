using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Data;

namespace Posetrix.Core.ViewModels;

public partial class DynamicViewModel : BaseViewModel
{
    [ObservableProperty] public partial ApplicationModelNames ModelName { get; set; }
}