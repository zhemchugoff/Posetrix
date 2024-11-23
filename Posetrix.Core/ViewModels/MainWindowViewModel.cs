using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace Posetrix.Core.ViewModels;

public partial class MainWindowViewModel: BaseViewModel
{
    public string WindowTitle { get; set; } = "Posetrix";

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
