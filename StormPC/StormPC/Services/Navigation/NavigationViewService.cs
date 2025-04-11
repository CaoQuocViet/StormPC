using System.Diagnostics.CodeAnalysis;

using Microsoft.UI.Xaml.Controls;

using StormPC.Contracts;
using StormPC.Helpers;
using StormPC.ViewModels;
using StormPC.ViewModels.Settings;
using StormPC.Contracts.Services;

namespace StormPC.Services;

/// <summary>
/// Dịch vụ quản lý NavigationView và điều hướng qua các mục menu
/// </summary>
public class NavigationViewService : INavigationViewService
{
    private readonly INavigationService _navigationService;
    private readonly IPageService _pageService;
    private NavigationView? _navigationView;

    /// <summary>
    /// Các mục menu được cấu hình trong NavigationView
    /// </summary>
    public IList<object>? MenuItems => _navigationView?.MenuItems;

    /// <summary>
    /// Mục cài đặt trong NavigationView
    /// </summary>
    public object? SettingsItem => _navigationView?.SettingsItem;

    /// <summary>
    /// Khởi tạo dịch vụ NavigationView
    /// </summary>
    public NavigationViewService(INavigationService navigationService, IPageService pageService)
    {
        _navigationService = navigationService;
        _pageService = pageService;
    }

    /// <summary>
    /// Khởi tạo NavigationView và đăng ký các sự kiện
    /// </summary>
    [MemberNotNull(nameof(_navigationView))]
    public void Initialize(NavigationView navigationView)
    {
        _navigationView = navigationView;
        _navigationView.BackRequested += OnBackRequested;
        _navigationView.ItemInvoked += OnItemInvoked;
    }

    /// <summary>
    /// Hủy đăng ký các sự kiện
    /// </summary>
    public void UnregisterEvents()
    {
        if (_navigationView != null)
        {
            _navigationView.BackRequested -= OnBackRequested;
            _navigationView.ItemInvoked -= OnItemInvoked;
        }
    }

    /// <summary>
    /// Lấy mục được chọn dựa trên loại trang
    /// </summary>
    public NavigationViewItem? GetSelectedItem(Type pageType)
    {
        if (_navigationView != null)
        {
            return GetSelectedItem(_navigationView.MenuItems, pageType) ?? GetSelectedItem(_navigationView.FooterMenuItems, pageType);
        }

        return null;
    }

    /// <summary>
    /// Xử lý sự kiện khi nút Back được nhấn
    /// </summary>
    private void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => _navigationService.GoBack();

    /// <summary>
    /// Xử lý sự kiện khi một mục menu được chọn
    /// </summary>
    private void OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        if (args.IsSettingsInvoked)
        {
            _navigationService.NavigateTo(typeof(SettingsViewModel).FullName!);
        }
        else
        {
            var selectedItem = args.InvokedItemContainer as NavigationViewItem;

            if (selectedItem?.GetValue(NavigationHelper.NavigateToProperty) is string pageKey)
            {
                _navigationService.NavigateTo(pageKey);
            }
        }
    }

    /// <summary>
    /// Tìm mục được chọn trong danh sách menu
    /// </summary>
    private NavigationViewItem? GetSelectedItem(IEnumerable<object> menuItems, Type pageType)
    {
        foreach (var item in menuItems.OfType<NavigationViewItem>())
        {
            if (IsMenuItemForPageType(item, pageType))
            {
                return item;
            }

            var selectedChild = GetSelectedItem(item.MenuItems, pageType);
            if (selectedChild != null)
            {
                return selectedChild;
            }
        }

        return null;
    }

    /// <summary>
    /// Kiểm tra xem một mục menu có phù hợp với loại trang không
    /// </summary>
    private bool IsMenuItemForPageType(NavigationViewItem menuItem, Type sourcePageType)
    {
        if (menuItem.GetValue(NavigationHelper.NavigateToProperty) is string pageKey)
        {
            return _pageService.GetPageType(pageKey) == sourcePageType;
        }

        return false;
    }
}
