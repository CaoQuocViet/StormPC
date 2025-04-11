using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace StormPC.Converters;

/// <summary>
/// Bộ chuyển đổi từ trạng thái đơn hàng sang kiểu dáng nút xóa
/// </summary>
public class OrderStatusToStyleConverter : IValueConverter
{
    /// <summary>
    /// Chuyển đổi chuỗi trạng thái đơn hàng thành kiểu dáng nút xóa tương ứng
    /// </summary>
    /// <param name="value">Chuỗi trạng thái đơn hàng</param>
    /// <param name="targetType">Kiểu dữ liệu đích</param>
    /// <param name="parameter">Tham số bổ sung</param>
    /// <param name="language">Ngôn ngữ</param>
    /// <returns>Style tương ứng cho nút xóa dựa trên trạng thái đơn hàng</returns>
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

    /// <summary>
    /// Chuyển đổi từ kiểu dáng nút xóa sang chuỗi trạng thái đơn hàng (không được hỗ trợ)
    /// </summary>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
} 