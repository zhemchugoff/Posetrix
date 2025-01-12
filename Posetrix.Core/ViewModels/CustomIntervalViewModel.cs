using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Models;

namespace Posetrix.Core.ViewModels;

public partial class CustomIntervalViewModel : DynamicViewModel
{
    public override string DisplayName => "Custom interval (in seconds)";

    [ObservableProperty]
    public partial int? Seconds { get; set; } = 0;

    partial void OnSecondsChanged(int? value)
    {
        if (string.IsNullOrWhiteSpace(value.ToString()))
        {
            Seconds = 0;
        }
    }

    public override int GetSeconds() => Seconds ?? 0;
}