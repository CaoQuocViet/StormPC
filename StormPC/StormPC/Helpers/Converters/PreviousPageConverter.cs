using Microsoft.UI.Xaml.Data;

namespace StormPC.Helpers;

public class PreviousPageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is int currentPage)
        {
            return currentPage - 1;
        }
        return 1;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
} 