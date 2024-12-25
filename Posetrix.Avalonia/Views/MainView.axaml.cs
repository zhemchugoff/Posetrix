using Avalonia.Controls;

namespace Posetrix.Avalonia.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();

        // Listen to ValueChanged to react to numeric value changes.
        NumericInput.ValueChanged += NumericInput_ValueChanged;
    }

    private void NumericInput_ValueChanged(object? sender, NumericUpDownValueChangedEventArgs e)
    {
        if (NumericInput.Value is null)
        {
            NumericInput.Value = 0;
        }
        else
        {
            var newValue = e.NewValue;

            if (newValue.HasValue && newValue.Value < 0)
            {
                NumericInput.Value = 0;
            }
        }
    }
}