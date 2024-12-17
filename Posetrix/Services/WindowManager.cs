using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;
using System.Windows;

namespace Posetrix.Services;

public class WindowManager(WindowMapper windowMapper) : IWindowManager
{
    private readonly WindowMapper _windowMapper = windowMapper;

    public void ShowWindow(BaseViewModel viewModel)
    {
        var windowType = _windowMapper.GetWindowTypeForViewModel(viewModel.GetType());

        if (windowType != null)
        {
            var window = Activator.CreateInstance(windowType) as Window;
            window.DataContext = viewModel;
            window.Show();
        }
    }

    public void ShowDialog(BaseViewModel viewModel)
    {
        var windowType = _windowMapper.GetWindowTypeForViewModel(viewModel.GetType());

        if (windowType != null)
        {
            var window = Activator.CreateInstance(windowType) as Window;
            window.DataContext = viewModel;
            window.ShowDialog();
        }
    }
}
