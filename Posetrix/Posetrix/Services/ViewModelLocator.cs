using Posetrix.Core.ViewModels;
using System.Diagnostics;

namespace Posetrix.Services;

/// <summary>
/// A ViewModel Locator <c>ViewModelLocator</c> for managing and locating application viewmodels.
/// </summary>
public class ViewModelLocator
{
    private readonly FolderBrowserService _folderBrowserService = new();

    public MainWindowViewModel MainWindowViewModel => new MainWindowViewModel();
    public FoldersAddWindowViewModel FoldersAddWindowViewModel => new FoldersAddWindowViewModel(_folderBrowserService);
    //public FoldersAddWindowViewModel FoldersAddWindowViewModel { get; set; }

    public PredefinedIntervalsViewModel PredefinedIntervalsViewModel { get; set; } = new PredefinedIntervalsViewModel();
    public CustomIntervalViewModel CustomIntervalViewModel { get; set; } = new CustomIntervalViewModel();

    public SettingsWindowViewModel SettingsWindowViewModel { get; set; } = new SettingsWindowViewModel();

    public PracticeModesViewModel PracticeModesViewModel => new PracticeModesViewModel(PredefinedIntervalsViewModel, CustomIntervalViewModel);
    public SessionViewModel SessionViewModel => new SessionViewModel(CustomIntervalViewModel, FoldersAddWindowViewModel);
    //public SessionViewModel SessionViewModel { get; set; }

    //public ViewModelLocator()
    //{
    //    FoldersAddWindowViewModel = new FoldersAddWindowViewModel(_folderBrowserService);
    //    SessionViewModel = new SessionViewModel(CustomIntervalViewModel, FoldersAddWindowViewModel);

    //}
}


