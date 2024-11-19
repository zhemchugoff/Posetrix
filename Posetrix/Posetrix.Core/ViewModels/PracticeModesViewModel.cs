using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Interfaces;
using Posetrix.Core.Models;
using System.Collections.ObjectModel;


namespace Posetrix.Core.ViewModels
{
    public partial class PracticeModesViewModel : BaseViewModel, IMyUserControl
    {
        [ObservableProperty]
        private ComboBoxItem? _selectedItem;

        [ObservableProperty]
        private object? _currentViewModel;
        public ObservableCollection<ComboBoxViewModel> ViewModelsCollection { get; set; }

        public string Name => "Practice Modes";

        public string? SelectedControl { get; set; }
        //public ObservableCollection<ComboBoxItem> Items { get; set; }


        public PracticeModesViewModel()
        {
            ViewModelsCollection = new ObservableCollection<ComboBoxViewModel>
            {
                new ComboBoxViewModel {ViewModelName = "Predefined intervals", ViewModelObject = new PredefinedIntervalsViewModel()},
                new ComboBoxViewModel {ViewModelName = "Custom Intervals", ViewModelObject = new CustomIntervalViewModel()}
            };

            CurrentViewModel = ViewModelsCollection[0];
            //Items = new ObservableCollection<ComboBoxItem>
            //{
            //    new ComboBoxItem {Name="Item 1", Content="Content for Item 1" },
            //    new ComboBoxItem {Name="Item 2", Content="Content for Item 2" },
            //    new ComboBoxItem {Name="Item 3", Content="Content for Item 3" }
            //};

    
        }

    }
}
