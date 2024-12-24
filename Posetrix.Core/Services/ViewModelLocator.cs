using Microsoft.Extensions.DependencyInjection;
using Posetrix.Core.ViewModels;

namespace Posetrix.Core.Services;

/// <summary>
/// Class <c>ViewModelLocator</c> gets required viewmodels from <c>IServiceProvider</c>.
/// </summary>
/// <param name="serviceProvider"></param>
public class ViewModelLocator(IServiceProvider serviceProvider)
{
    public MainViewModel MainViewModel => serviceProvider.GetRequiredService<MainViewModel>();
    public SettingsViewModel SettingsViewModel => serviceProvider.GetRequiredService<SettingsViewModel>();
    public FolderAddViewModel FolderAddViewModel => serviceProvider.GetRequiredService<FolderAddViewModel>();
    public CustomIntervalViewModel CustomIntervalViewModel => serviceProvider.GetRequiredService<CustomIntervalViewModel>();
    public PredefinedIntervalsViewModel PredefinedIntervalsViewModel => serviceProvider.GetRequiredService<PredefinedIntervalsViewModel>();
    public SessionViewModel SessionViewModel => serviceProvider.GetRequiredService<SessionViewModel>();
}
