using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;


namespace Posetrix.Core.ViewModels
{
    public partial class PracticeModesViewModel : BaseViewModel, IMyUserControl
    {
        private readonly CustomIntervalViewModel _customIntervalViewModel;
        private readonly PredefinedIntervalsViewModel _predefinedIntervalsViewModel;

        [ObservableProperty]
        private ComboBoxItem? _selectedItem;

        [ObservableProperty]
        private object? _currentViewModel;
        public ObservableCollection<ComboBoxViewModel> ViewModelsCollection { get; set; }

        public string Name => "Practice Modes";

        public string? SelectedControl { get; set; }


        public PracticeModesViewModel(PredefinedIntervalsViewModel predefinedIntervalsViewModel, CustomIntervalViewModel customIntervalViewModel)
        {
            _customIntervalViewModel = customIntervalViewModel;
            _predefinedIntervalsViewModel = predefinedIntervalsViewModel;

            ViewModelsCollection = new ObservableCollection<ComboBoxViewModel>
            {
                new ComboBoxViewModel {ViewModelName = "Predefined intervals", ViewModelObject = _predefinedIntervalsViewModel},
                new ComboBoxViewModel {ViewModelName = "Custom Intervals", ViewModelObject = _customIntervalViewModel}
            };

            CurrentViewModel = ViewModelsCollection[0];
        }

    }
}
