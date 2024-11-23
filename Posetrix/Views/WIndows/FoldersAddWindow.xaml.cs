using System.Windows;
using Posetrix.Core.Interfaces;
using Posetrix.Core.ViewModels;

namespace Posetrix.Views;

/// <summary>
/// Interaction logic for AddReferenceFoldersWindow.xaml
/// </summary>
public partial class FoldersAddWindow : Window
{
    public FoldersAddWindow(FoldersAddWindowViewModel foldersAddWindowViewModel)
    {
        InitializeComponent();
        DataContext = foldersAddWindowViewModel;
    }
}
