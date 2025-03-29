using Microsoft.UI.Xaml.Data;
using System;

namespace StormPC.Helpers;

public class CurrencyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is decimal decimalValue)
        {
            return FormatCurrency((double)decimalValue);
        }
        else if (value is double doubleValue)
        {
            return FormatCurrency(doubleValue);
        }
        else if (value is int intValue)
        {
            return FormatCurrency(intValue);
        }
        else if (value is long longValue)
        {
            return FormatCurrency(longValue);
        }

        return "0 VNĐ";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }

    private string FormatCurrency(double value)
    {
        if (value >= 1_000_000_000) // Tỷ
        {
            return $"{value / 1_000_000_000:N1} Tỷ VNĐ";
        }
        else if (value >= 1_000_000) // Triệu
        {
            return $"{value / 1_000_000:N0} Tr VNĐ";
        }
        else // Dưới triệu
        {
            return $"{value:N0} VNĐ";
        }
    }
} 