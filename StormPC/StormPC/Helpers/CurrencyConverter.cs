using Microsoft.UI.Xaml.Data;
using StormPC.Core.Helpers;

namespace StormPC.Helpers;

public class CurrencyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is decimal decimalValue)
        {
            return CurrencyHelper.FormatCurrency(decimalValue);
        }
        
        if (value is double doubleValue)
        {
            return CurrencyHelper.FormatCurrency((decimal)doubleValue);
        }
        
        if (value is int intValue)
        {
            return CurrencyHelper.FormatCurrency(intValue);
        }

        return value?.ToString() ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
} 