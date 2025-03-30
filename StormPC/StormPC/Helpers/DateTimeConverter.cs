using Microsoft.UI.Xaml.Data;
using System;

namespace StormPC.Helpers;

public class DateTimeConverter : IValueConverter
{
    public static string Format(DateTime value)
    {
        return value.ToString("dd/MM/yyyy");
    }

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is DateTime dateTime)
        {
            return Format(dateTime);
        }
        
        if (value is DateTimeOffset dateTimeOffset)
        {
            return Format(dateTimeOffset.DateTime);
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
} 