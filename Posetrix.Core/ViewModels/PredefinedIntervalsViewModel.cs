using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;

namespace Posetrix.Core.ViewModels;

public partial class PredefinedIntervalsViewModel : BaseViewModel, IDynamicViewModel
{
    public string DisplayName => "Predefined intervals";

    [ObservableProperty]
    private Intervals _selectedInterval;

    public SessionTimer GetTimer() =>

        SelectedInterval switch
        {
            Intervals.Interval1 => new() { Hours = 0, Minutes = 0, Seconds = 30 },
            Intervals.Interval2 => new() { Hours = 0, Minutes = 0, Seconds = 45 },
            Intervals.Interval3 => new() { Hours = 0, Minutes = 1, Seconds = 0 },
            Intervals.Interval4 => new() { Hours = 0, Minutes = 2, Seconds = 0 },
            Intervals.Interval5 => new() { Hours = 0, Minutes = 5, Seconds = 0 },
            Intervals.Interval6 => new() { Hours = 0, Minutes = 10, Seconds = 0 },
            _ => new() { Hours = 0, Minutes = 0, Seconds = 15 }
        };

}

public enum Intervals
{
    Interval1, // 30s
    Interval2, // 45s
    Interval3, // 1m
    Interval4, // 2m
    Interval5, // 5m
    Interval6  // 10m
}
