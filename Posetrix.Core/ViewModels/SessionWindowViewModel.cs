using CommunityToolkit.Mvvm.ComponentModel;
using Posetrix.Core.Interfaces;
using System.Collections.ObjectModel;

namespace Posetrix.Core.ViewModels;

public partial class SessionWindowViewModel : BaseViewModel, ICustomWindow
{
    public string WindowTitle => "Drawing session";

    private readonly MainWindowViewModel _mainWindowViewModel;

    [ObservableProperty]
    private int _timer;

    [ObservableProperty]
    private int _seconds;

    private readonly ObservableCollection<string> _sessionImages;
    [ObservableProperty]
    private string? _currentImage;


    public SessionWindowViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _sessionImages = new ObservableCollection<string>();
        PopulateSessionImagesList();
        _currentImage = mainWindowViewModel.ReferenceFolders[0].References[1];
        this._mainWindowViewModel = mainWindowViewModel;
    }

    private void PopulateSessionImagesList()
    {
        foreach (var referenceFolder in _mainWindowViewModel.ReferenceFolders)
        {
            foreach (var image in referenceFolder.References)
            {
                _sessionImages.Add(image);
            }
        }
    }
}
