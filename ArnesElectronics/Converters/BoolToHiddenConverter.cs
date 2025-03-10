using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace ArnesElectronics.Converters;

public class BoolToHiddenConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
            return !boolValue; // Opposite visibility logic
        return true; // Default: Hide if not a boolean
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
