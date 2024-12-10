using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Data;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;

namespace Posetrix.Core.ViewModels
{
    public partial class PredefinedIntervalsViewModel : BaseViewModel, IDynamicViewModel
    {
        [ObservableProperty] int _interval1 = 30;
        public string DisplayName => "Predefined intervals";

        public SessionTimer SessionTimer => new() { Hours = 0, Minutes = 0, Seconds = Interval1 };
    }
}
