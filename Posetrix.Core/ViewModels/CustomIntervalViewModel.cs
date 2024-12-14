using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Posetrix.Core.ViewModels;

public partial class CustomIntervalViewModel : ObservableValidator, IDynamicViewModel
{
    public string DisplayName => "Custom interval";

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(0, int.MaxValue, ErrorMessage = "Enter a number between 0 and whatever you want")]
    private int _hours;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(0, 59, ErrorMessage = "Enter a number between 0 and 59")]
    private int _minutes;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(0, 59, ErrorMessage = "Enter a number between 0 and 59")]
    private int _seconds;

    public SessionTimer GetTimer() => new SessionTimer() { Hours = Hours, Minutes = Minutes, Seconds = Seconds };

    public bool CanStart => !HasErrors;

}