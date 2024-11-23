using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Posetrix.Core.Interfaces;


namespace Posetrix.Core.ViewModels;

public partial class MainWindowViewModel: BaseViewModel, ICustomWindow
{
    public string WindowTitle => "Posetrix";

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
