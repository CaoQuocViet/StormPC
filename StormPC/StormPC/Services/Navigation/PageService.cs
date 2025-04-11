using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Controls;

using StormPC.Contracts;
using StormPC.ViewModels.ActivityLog;
using StormPC.ViewModels.BaseData;
using StormPC.ViewModels.Dashboard;
using StormPC.ViewModels.Orders;
using StormPC.ViewModels.Settings;
using StormPC.ViewModels.Shell;
using StormPC.Views.ActivityLog;
using StormPC.Views.BaseData;
using StormPC.Views.Dashboard;
using StormPC.Views.Orders;
using StormPC.Views.Settings;
using StormPC.Views.Shell;

namespace StormPC.Services;

/// <summary>
/// Dịch vụ quản lý ánh xạ giữa ViewModel và Page
/// </summary>
public class PageService : IPageService
{
    private readonly Dictionary<string, Type> _pages = new();

    /// <summary>
    /// Khởi tạo dịch vụ và cấu hình ánh xạ giữa các ViewModel và Page
    /// </summary>
    public PageService()
    {
        Configure<ShellViewModel, ShellPage>();
        
        // Các trang báo cáo
        Configure<InventoryReportViewModel, InventoryReportPage>();
        Configure<RevenueReportViewModel, RevenueReportPage>();
        Configure<CustomerReportViewModel, CustomerReportPage>();
        
        // Các trang dữ liệu cơ sở
        Configure<CategoriesViewModel, CategoriesPage>();
        Configure<ProductsViewModel, ProductsPage>();
        
        // Các trang đơn hàng
        Configure<OrderListViewModel, OrderListPage>();
        Configure<OrderDetailViewModel, OrderDetailPage>();
        
        // Trang nhật ký hoạt động
        Configure<ActivityLogViewModel, ActivityLogPage>();
        
        // Trang cài đặt
        Configure<SettingsViewModel, SettingsPage>();
    }

    /// <summary>
    /// Lấy loại trang dựa trên khóa (tên ViewModel)
    /// </summary>
    public Type GetPageType(string key)
    {
        Type? pageType;
        lock (_pages)
        {
            if (!_pages.TryGetValue(key, out pageType))
            {
                throw new ArgumentException($"Không tìm thấy trang: {key}. Bạn có quên gọi PageService.Configure không?");
            }
        }

        return pageType;
    }

    /// <summary>
    /// Cấu hình ánh xạ giữa ViewModel và Page
    /// </summary>
    private void Configure<VM, V>()
        where VM : ObservableObject
        where V : Page
    {
        lock (_pages)
        {
            var key = typeof(VM).FullName!;
            if (_pages.ContainsKey(key))
            {
                throw new ArgumentException($"Khóa {key} đã được cấu hình trong PageService");
            }

            var type = typeof(V);
            if (_pages.ContainsValue(type))
            {
                throw new ArgumentException($"Loại này đã được cấu hình với khóa {_pages.First(p => p.Value == type).Key}");
            }

            _pages.Add(key, type);
        }
    }
}
