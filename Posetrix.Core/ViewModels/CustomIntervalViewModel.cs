using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Data;

namespace Posetrix.Core.ViewModels;

public partial class CustomIntervalViewModel : DynamicViewModel
{
    [ObservableProperty] private int _hours;
    [ObservableProperty] private int _minutes;
    [ObservableProperty] private int _seconds;
    
    public CustomIntervalViewModel()
    {
        ModelName = ApplicationModelNames.CustomInterval;
    }
}