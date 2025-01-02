using System.Globalization;
using System.Windows.Data;

namespace Posetrix.Converters;

public class PercentToValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double actualValue && parameter is string percentString && double.TryParse(percentString, out double percent))
        {
            return actualValue * (percent / 100);
        }
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
