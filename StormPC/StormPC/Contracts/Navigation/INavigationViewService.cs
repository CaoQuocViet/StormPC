using Microsoft.UI.Xaml.Controls;

namespace StormPC.Contracts;

public interface INavigationViewService
{
    /// <summary>
    /// Danh sách các mục menu trong NavigationView
    /// </summary>
    IList<object>? MenuItems
    {
        get;
    }

    /// <summary>
    /// Mục cài đặt trong NavigationView
    /// </summary>
    object? SettingsItem
    {
        get;
    }

    /// <summary>
    /// Khởi tạo dịch vụ với NavigationView cụ thể
    /// </summary>
    void Initialize(NavigationView navigationView);

    /// <summary>
    /// Hủy đăng ký các sự kiện
    /// </summary>
    void UnregisterEvents();

    /// <summary>
    /// Lấy mục đã chọn dựa trên loại trang
    /// </summary>
    NavigationViewItem? GetSelectedItem(Type pageType);
}
