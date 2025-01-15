using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Enums;

namespace Posetrix.Core.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty] public partial ViewModelNames ViewModelName { get; set; }
}
