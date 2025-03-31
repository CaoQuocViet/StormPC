using Microsoft.UI.Xaml.Data;
using System;

namespace StormPC.Helpers;

public class PercentageConverter : IValueConverter
{
    public static string Format(double value)
    {
        return $"{value:N2}%";
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
        
        if (value is float floatValue)
        {
            return Format(floatValue);
        }

        return "0%";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
} 