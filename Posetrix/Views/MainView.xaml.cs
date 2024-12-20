using Posetrix.Core.Data;
using System.Text.RegularExpressions;
using System.Windows;

namespace Posetrix.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
    }

    private void ImageCount_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        Regex regex = TimerRegex.TimerValuesRegex();
        e.Handled = !regex.IsMatch(e.Text);
    }
}