using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Posetrix.Avalonia.Views;
using Posetrix.Core.ViewModels;

namespace Posetrix.Avalonia.Services;

public class WindowMapper
{
    private readonly Dictionary<Type, Type> _mappings = [];

    public WindowMapper()
    {
        RegisterMapping<MainViewModel, MainView>();
        RegisterMapping<SettingsViewModel, SettingsView>();
        RegisterMapping<SessionViewModel, SessionView>();
        RegisterMapping<FolderAddViewModel, FoldersAddView>();
    }

    public void RegisterMapping<TViewModel, TWindow>() where TViewModel : BaseViewModel where TWindow : Window
    {
        _mappings[typeof(TViewModel)] = typeof(TWindow);
    }

    public Type? GetWindowTypeForViewModel(Type viewModelType)
    {
        _mappings.TryGetValue(viewModelType, out var windowType);
        return windowType;
    }
}