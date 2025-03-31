using Microsoft.UI.Xaml.Data;

namespace StormPC.Helpers;

public class NumberFormatConverter : IValueConverter
{
    public static string Format(double value)
    {
        return value.ToString("N0");
    }

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is decimal decimalValue)
        {
            return Format((double)decimalValue);
        }
        
        if (value is double doubleValue)
        {
            return Format(doubleValue);
        }
        
        if (value is int intValue)
        {
            return Format(intValue);
        }

        return value?.ToString() ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
} 