using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace StormPC.Converters;

public class OrderStatusToStyleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string status)
        {
            return status == "Cancelled" 
                ? Application.Current.Resources["DeleteButtonEnabledStyle"] 
                : Application.Current.Resources["DeleteButtonStyle"];
        }
        return Application.Current.Resources["DeleteButtonStyle"];
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
} 