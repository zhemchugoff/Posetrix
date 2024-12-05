using System.Globalization;
using System.Windows.Data;

namespace Posetrix.Converters;

public class BooleanToXScaleConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolean)
        {
            return boolean ? -1 : 1;
        }

        return 1;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}