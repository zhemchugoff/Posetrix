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
        // Allow only numeric input
        e.Handled = !IsTextNumeric(e.Text);
    }

    // Accept only digits.
    private static bool IsTextNumeric(string text) => Regex.IsMatch(text, "^[0-9]+$");
}
