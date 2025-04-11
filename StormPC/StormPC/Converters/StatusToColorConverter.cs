using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace StormPC.Converters
{
    /// <summary>
    /// Bộ chuyển đổi trạng thái thành màu sắc tương ứng
    /// </summary>
    public class StatusToColorConverter : IValueConverter
    {
        /// <summary>
        /// Chuyển đổi chuỗi trạng thái thành màu sắc tương ứng
        /// </summary>
        /// <param name="value">Chuỗi trạng thái cần chuyển đổi</param>
        /// <param name="targetType">Kiểu dữ liệu đích</param>
        /// <param name="parameter">Tham số bổ sung</param>
        /// <param name="language">Ngôn ngữ</param>
        /// <returns>SolidColorBrush tương ứng với trạng thái</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string status)
            {
                return status switch
                {
                    "Success" => new SolidColorBrush(Color.FromArgb(32, 40, 167, 69)),   
                    "Info" => new SolidColorBrush(Color.FromArgb(32, 255, 193, 7)),      
                    "Error" => new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)),
                    "Failed" => new SolidColorBrush(Color.FromArgb(32, 220, 53, 69)),
                    "Warning" => new SolidColorBrush(Color.FromArgb(255, 255, 165, 0)),
                    _ => new SolidColorBrush(Colors.Transparent)
                };
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        /// <summary>
        /// Chuyển đổi từ màu sắc sang chuỗi trạng thái (không được hỗ trợ)
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
} 