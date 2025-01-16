using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Enums;
using Posetrix.Core.Interfaces;

namespace Posetrix.Core.ViewModels;

public partial class CustomIntervalViewModel : BaseViewModel, IDynamicViewModel
{
    public string DisplayName => "Custom interval (in seconds)";

    [ObservableProperty] public partial int? Seconds { get; set; } = 0;

    public CustomIntervalViewModel()
    {
        ViewModelName = ViewModelNames.CustomInterval;
    }
    partial void OnSecondsChanged(int? value)
    {
        if (string.IsNullOrWhiteSpace(value.ToString()))
        {
            Seconds = 0;
        }
    }

    public int GetSeconds()
    {
        if (Seconds is null || Seconds < 0)
        {
            return 0;
        }

        return (int)Seconds;
    }
}