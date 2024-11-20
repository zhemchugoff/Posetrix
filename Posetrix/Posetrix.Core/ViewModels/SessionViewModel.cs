﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Posetrix.Core.ViewModels;

public partial class SessionViewModel : BaseViewModel
{
    private CustomIntervalViewModel _customInterval;
    private FoldersAddWindowViewModel _foldersAddWindow;

    [ObservableProperty]
    private int _timer;

    [ObservableProperty]
    private int _seconds;

    //public int Id { get; set; } = 2;

    private readonly ObservableCollection<string> _sessionImages;

    [ObservableProperty]
    private string? _currentImage;

    public SessionViewModel(CustomIntervalViewModel customInterval, FoldersAddWindowViewModel foldersAddWindow)
    {
        _customInterval = customInterval;

        if (foldersAddWindow != null) { 
        _foldersAddWindow = foldersAddWindow;
        }


        _timer = _customInterval.SessionTimer.Seconds;
        _sessionImages = new ObservableCollection<string>();
        PopulateSessionImagesList();
        _currentImage = _foldersAddWindow.ReferenceFolders[0].References[0];
    }

    private void PopulateSessionImagesList()
    {
        foreach (var referenceFolder in _foldersAddWindow.ReferenceFolders) { 
            foreach (var image in referenceFolder.References)
            {
                _sessionImages.Add(image);
            }
        }
    }
}
