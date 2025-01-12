using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Interfaces;

namespace Posetrix.Core.ViewModels;

public partial class CustomIntervalViewModel : ObservableObject, IDynamicViewModel
{
    public string DisplayName => "Custom interval (in seconds)";

    [ObservableProperty]
    public partial int? Seconds { get; set; } = 0;

    partial void OnSecondsChanged(int? value)
    {
        if (string.IsNullOrWhiteSpace(value.ToString()))
        {
            Seconds = 0;
        }
    }

    public int GetSeconds() => Seconds ?? 0;
}