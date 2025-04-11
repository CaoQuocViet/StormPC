using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace StormPC.Converters;

/// <summary>
/// Bộ chuyển đổi chuyển chuỗi thành trạng thái hiển thị
/// </summary>
public class StringToVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Chuyển đổi chuỗi thành trạng thái hiển thị
    /// </summary>
    /// <param name="value">Giá trị chuỗi cần chuyển đổi</param>
    /// <param name="targetType">Kiểu dữ liệu đích</param>
    /// <param name="parameter">Tham số bổ sung</param>
    /// <param name="language">Ngôn ngữ</param>
    /// <returns>Visibility.Visible nếu chuỗi có giá trị, ngược lại trả về Visibility.Collapsed</returns>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string str)
        {
            return string.IsNullOrWhiteSpace(str) ? Visibility.Collapsed : Visibility.Visible;
        }
        return Visibility.Collapsed;
    }

    /// <summary>
    /// Chuyển đổi từ trạng thái hiển thị sang chuỗi (không được hỗ trợ)
    /// </summary>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
} 