using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Enums;
using Posetrix.Core.Interfaces;

namespace Posetrix.Core.ViewModels;

public partial class PredefinedIntervalsViewModel : BaseViewModel, IDynamicViewModel
{
    public string DisplayName => "Predefined intervals";

    [ObservableProperty] public partial Intervals SelectedInterval { get; set; }

    public PredefinedIntervalsViewModel()
    {
        ViewModelName = ViewModelNames.PredefinedIntervals;
    }

    public int GetSeconds() =>

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