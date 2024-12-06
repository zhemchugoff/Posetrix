using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Data;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;

namespace Posetrix.Core.ViewModels
{
    public partial class CustomIntervalViewModel : DynamicViewModel
    {
        [ObservableProperty] private SessionTimer _sessionTimer;
        
        public CustomIntervalViewModel()
        {
            ModelName = ApplicationModelNames.CustomInterval;
            
            _sessionTimer = new SessionTimer
            {
                Hours = 0,
                Minutes = 0,
                Seconds = 30
            };
        }
    }
}