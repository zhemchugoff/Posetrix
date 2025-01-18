using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Enums;
using Posetrix.Core.Interfaces;

namespace Posetrix.Core.ViewModels;

public partial class CustomIntervalViewModel : BaseViewModel, IDynamicViewModel
{
    private readonly ISharedSessionParametersService _sharedSessionParametersService;

    public string DisplayName => "Custom interval (in seconds)";

    [ObservableProperty] public partial int? Seconds { get; set; } = 0;

    public CustomIntervalViewModel(ISharedSessionParametersService sharedSessionParametersService)
    {
        ViewModelName = ViewModelNames.CustomInterval;
        _sharedSessionParametersService = sharedSessionParametersService;
        Seconds = _sharedSessionParametersService.Seconds;
    }
    partial void OnSecondsChanged(int? value)
    {
        if (string.IsNullOrWhiteSpace(value.ToString()))
        {
            Seconds = 0;
        }

        if (Seconds < 0)
        {
            Seconds = 0;
        }

        _sharedSessionParametersService.Seconds = value;
    }
}