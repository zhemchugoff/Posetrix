using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.ViewModels;

namespace Posetrix.Avalonia.Views;

public partial class CustomIntervalView : UserControl
{
    public CustomIntervalView()
    {
        InitializeComponent();
        var customInterval = App.ServiceProvider.GetRequiredService<CustomIntervalView>();
        customInterval.DataContext = App.ServiceProvider.GetRequiredService<CustomIntervalViewModel>();
    }
}