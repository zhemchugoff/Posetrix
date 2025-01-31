using System.Diagnostics;
using System.Windows.Input;

namespace Posetrix.Views;

/// <summary>
/// Interaction logic for AddReferenceFoldersWindow.xaml
/// </summary>
public partial class FoldersAddView
{
    public FoldersAddView()
    {
        InitializeComponent();
    }

    private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
        {
            e.Handled = true; // Prevent space from activating buttons.
        }
    }
}
