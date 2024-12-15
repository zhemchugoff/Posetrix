using Posetrix.Core.Data;
using System.Text.RegularExpressions;

namespace Posetrix.Views;

/// <summary>
/// Interaction logic for CustomInterval.xaml
/// </summary>
public partial class CustomIntervalView
{
    public CustomIntervalView()
    { 
        InitializeComponent();
    }

    private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        Regex regex = TimerRegex.TimerValuesRegex();
        e.Handled = !regex.IsMatch(e.Text);
    }
}
