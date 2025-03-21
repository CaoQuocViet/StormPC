using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Controls;

using StormPC.Contracts.Services;
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

public class PageService : IPageService
{
    private readonly Dictionary<string, Type> _pages = new();

    public PageService()
    {
        Configure<ShellViewModel, ShellPage>();
        
        // Dashboard pages
        Configure<DashboardViewModel, DashboardPage>();
        Configure<InventoryReportViewModel, InventoryReportPage>();
        Configure<RevenueReportViewModel, RevenueReportPage>();
        Configure<CustomerReportViewModel, CustomerReportPage>();
        
        // Base Data pages
        Configure<CategoriesViewModel, CategoriesPage>();
        Configure<ProductsViewModel, ProductsPage>();
        
        // Orders pages
        Configure<OrderListViewModel, OrderListPage>();
        Configure<OrderDetailViewModel, OrderDetailPage>();
        
        // Activity Log page
        Configure<ActivityLogViewModel, ActivityLogPage>();
        
        // Settings page
        Configure<SettingsViewModel, SettingsPage>();
    }

    public Type GetPageType(string key)
    {
        Type? pageType;
        lock (_pages)
        {
            if (!_pages.TryGetValue(key, out pageType))
            {
                throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
            }
        }

        return pageType;
    }

    private void Configure<VM, V>()
        where VM : ObservableObject
        where V : Page
    {
        lock (_pages)
        {
            var key = typeof(VM).FullName!;
            if (_pages.ContainsKey(key))
            {
                throw new ArgumentException($"The key {key} is already configured in PageService");
            }

            var type = typeof(V);
            if (_pages.ContainsValue(type))
            {
                throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == type).Key}");
            }

            _pages.Add(key, type);
        }
    }
}
