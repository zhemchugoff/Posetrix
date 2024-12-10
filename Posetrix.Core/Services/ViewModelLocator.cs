using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.ViewModels;

namespace Posetrix.Core.Services;

public class ViewModelLocator(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public MainViewModel MainViewModel => _serviceProvider.GetRequiredService<MainViewModel>();
    public SettingsViewModel SettingsViewModel => _serviceProvider.GetRequiredService<SettingsViewModel>();
    public FolderAddViewModel FolderAddViewModel => _serviceProvider.GetRequiredService<FolderAddViewModel>();
    public CustomIntervalViewModel CustomIntervalViewModel => _serviceProvider.GetRequiredService<CustomIntervalViewModel>();
    public PredefinedIntervalsViewModel PredefinedIntervalsViewModel => _serviceProvider.GetRequiredService<PredefinedIntervalsViewModel>();
    public SessionViewModel SessionViewModel => _serviceProvider.GetRequiredService<SessionViewModel>();
}
