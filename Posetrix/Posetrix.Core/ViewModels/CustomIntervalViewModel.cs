using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;

namespace Posetrix.Core.ViewModels
{
    public partial class CustomIntervalViewModel : BaseViewModel, IMyUserControl
    {
        public string Name => "Custom Intervals";

        [ObservableProperty]
        private SessionTimer _sessionTimer;

        public CustomIntervalViewModel()
        {
            _sessionTimer = new SessionTimer();
            _sessionTimer.Hours = 0;
            _sessionTimer.Minutes = 0;
            _sessionTimer.Seconds = 0;
        }
    }
}
