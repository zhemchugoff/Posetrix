using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Data;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;

namespace Posetrix.Core.ViewModels;

public partial class CustomIntervalViewModel : BaseViewModel, IDynamicViewModel
{
    public string DisplayName => "Custom interval";

    [ObservableProperty] private int _hours = 0;
    [ObservableProperty] private int _minutes = 0;
    [ObservableProperty] private int _seconds = 0;

    public SessionTimer GetTimer() => new() { Hours = Hours, Minutes = Minutes, Seconds = Seconds };
}