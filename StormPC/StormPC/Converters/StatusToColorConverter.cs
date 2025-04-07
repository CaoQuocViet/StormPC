using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace StormPC.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string status)
            {
                return status switch
                {
                    "Success" => new SolidColorBrush(Color.FromArgb(32, 40, 167, 69)),    // #2028a745
                    "Info" => new SolidColorBrush(Color.FromArgb(32, 255, 193, 7)),       // #20ffc107
                    "Error" => new SolidColorBrush(Color.FromArgb(32, 220, 53, 69)),      // #20dc3545
                    _ => new SolidColorBrush(Colors.Transparent)
                };
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
} 