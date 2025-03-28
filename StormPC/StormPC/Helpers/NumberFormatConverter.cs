using Microsoft.UI.Xaml.Data;

namespace StormPC.Helpers;

public class NumberFormatConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is decimal decimalValue)
        {
            return decimalValue.ToString("N0");
        }
        
        if (value is double doubleValue)
        {
            return doubleValue.ToString("N0");
        }
        
        if (value is int intValue)
        {
            return intValue.ToString("N0");
        }

        return value?.ToString() ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
} 