using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Enums;
using Posetrix.Core.Interfaces;

namespace Posetrix.Core.ViewModels;

public partial class PredefinedIntervalsViewModel : BaseViewModel, IDynamicViewModel
{
    public string DisplayName => "Predefined intervals";

    private readonly ISharedSessionParametersService _sharedSessionParametersService;
    [ObservableProperty] public partial Intervals SelectedInterval { get; set; } = Intervals.Interval1;

    public PredefinedIntervalsViewModel(ISharedSessionParametersService sharedSessionParametersService)
    {
        ViewModelName = ViewModelNames.PredefinedIntervals;
        _sharedSessionParametersService = sharedSessionParametersService;
        _sharedSessionParametersService.Seconds = ConvertEnumToSeconds(SelectedInterval);
    }

    public static int ConvertEnumToSeconds(Intervals interval) =>
        interval switch
        {
            Intervals.Interval1 => 30,
            Intervals.Interval2 => 45,
            Intervals.Interval3 => 60,
            Intervals.Interval4 => 120,
            Intervals.Interval5 => 300,
            Intervals.Interval6 => 600,
            _ => 0
        };

    partial void OnSelectedIntervalChanged(Intervals value)
    {
        _sharedSessionParametersService.Seconds = ConvertEnumToSeconds(value);
    }
}