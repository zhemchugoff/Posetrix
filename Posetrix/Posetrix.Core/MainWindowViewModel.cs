using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Posetrix.Core;

public partial class MainWindowViewModel: ObservableObject
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
