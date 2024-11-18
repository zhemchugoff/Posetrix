using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace Posetrix.Core.ViewModels;

public partial class MainWindowViewModel: BaseViewModel
{


    [ObservableProperty]
    public int counter;

    [RelayCommand]
    public void Increment()
    {
        Counter++;
    }

    [RelayCommand]
    public void Decrement()
    {
        Counter--;
    }
}
