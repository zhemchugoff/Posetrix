using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;

namespace Posetrix.Avalonia.Services;

public class WindowManager(WindowMapper windowMapper) : IWindowManager
{
    public void ShowWindow(BaseViewModel viewModel)
    {
        var windowType = windowMapper.GetWindowTypeForViewModel(viewModel.GetType());

        if (windowType != null)
        {
            if (Activator.CreateInstance(windowType) is Window window)
            {
                window.DataContext = viewModel;

                if (window.DataContext is IDisposable disposable)
                {
                    window.Unloaded += (s, e) => disposable.Dispose();
                }

                window.Show();
            }
        }
    }

    public void ShowDialog(BaseViewModel viewModel)
    {
        var windowType = windowMapper.GetWindowTypeForViewModel(viewModel.GetType());

        if (windowType != null)
        {
            if (Activator.CreateInstance(windowType) is Window window)
            {
                window.DataContext = viewModel;

                if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime appLifetime)
                {
                    if (appLifetime.MainWindow != null) window.ShowDialog(appLifetime.MainWindow);
                }
            }
        }
    }
}