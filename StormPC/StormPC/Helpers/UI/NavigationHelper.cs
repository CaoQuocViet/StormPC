using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace StormPC.Helpers;

/// <summary>
/// Lớp trợ giúp đặt đích điều hướng cho NavigationViewItem.
///
/// Sử dụng trong XAML:
/// <NavigationViewItem x:Uid="Shell_Main" Icon="Document" helpers:NavigationHelper.NavigateTo="AppName.ViewModels.MainViewModel" />
///
/// Sử dụng trong mã:
/// NavigationHelper.SetNavigateTo(navigationViewItem, typeof(MainViewModel).FullName);
/// </summary>
public class NavigationHelper
{
    /// <summary>
    /// Lấy đích điều hướng từ NavigationViewItem
    /// </summary>
    public static string GetNavigateTo(NavigationViewItem item) => (string)item.GetValue(NavigateToProperty);

    /// <summary>
    /// Đặt đích điều hướng cho NavigationViewItem
    /// </summary>
    public static void SetNavigateTo(NavigationViewItem item, string value) => item.SetValue(NavigateToProperty, value);

    /// <summary>
    /// Thuộc tính phụ thuộc cho NavigateTo
    /// </summary>
    public static readonly DependencyProperty NavigateToProperty =
        DependencyProperty.RegisterAttached("NavigateTo", typeof(string), typeof(NavigationHelper), new PropertyMetadata(null));
}
