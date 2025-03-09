using System;
using Avalonia.Data.Converters;

namespace ArnesElectronics.Converters
{
    public class IntToVisibilityConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue > 0; // Returns true (visible) if there are subitems
            }
            return false; // Hide if count is 0 or null
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}