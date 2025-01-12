using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Models;

namespace Posetrix.Core.ViewModels;

public partial class PredefinedIntervalsViewModel : DynamicViewModel
{
    public override string DisplayName { get => "Predefined intervals"; }

    [ObservableProperty]
    public partial Intervals SelectedInterval { get; set; }
    public override int GetSeconds() =>

        SelectedInterval switch
        {
            Intervals.Interval1 => 30,
            Intervals.Interval2 => 45,
            Intervals.Interval3 => 60,
            Intervals.Interval4 => 120,
            Intervals.Interval5 => 300,
            Intervals.Interval6 => 600,
            _ => 0
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
