using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Posetrix.Core.ViewModels;

public partial class CustomIntervalViewModel : ObservableValidator, IDynamicViewModel
{
    public string DisplayName => "Custom interval (in seconds)";

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(0, int.MaxValue, ErrorMessage = "Enter a correct number")]
    public partial int? Seconds { get; set; } = 0;

    partial void OnSecondsChanged(int? value)
    {
        if (string.IsNullOrWhiteSpace(value.ToString()))
        {
            Seconds = 0;
        }
    }

    public int GetSeconds() => Seconds ?? 0;
    public bool CanStart => !HasErrors;
}