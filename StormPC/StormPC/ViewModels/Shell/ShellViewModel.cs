using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using StormPC.Contracts;
using StormPC.Helpers;
using StormPC.ViewModels.Dashboard;
using StormPC.ViewModels.BaseData;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using StormPC.Contracts.Services;
using StormPC.Core.Services.System;
using StormPC.Views.Shell;

namespace StormPC.ViewModels.Shell;

public partial class ShellViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly INavigationViewService _navigationViewService;
    private readonly ILastPageService _lastPageService;
    private readonly ISearchService _searchService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private bool _isBackEnabled;

    [ObservableProperty]
    private object? _selected;

    [ObservableProperty]
    private string _searchQuery = string.Empty;

    public IList<object>? MenuItems => _navigationViewService.MenuItems;

    public object? SettingsItem => _navigationViewService.SettingsItem;

    public INavigationViewService NavigationViewService => _navigationViewService;

    public ShellViewModel(
        INavigationService navigationService, 
        INavigationViewService navigationViewService, 
        ILastPageService lastPageService, 
        ISearchService searchService,
        IDialogService dialogService)
    {
        _navigationService = navigationService;
        _navigationViewService = navigationViewService;
        _lastPageService = lastPageService;
        _searchService = searchService;
        _dialogService = dialogService;

        _navigationService.Navigated += OnNavigated;
    }

    // Xử lý sự kiện điều hướng
    private async void OnNavigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = _navigationService.CanGoBack;

        if (e.SourcePageType != null)
        {
            var viewModelTypeName = e.SourcePageType.FullName!.Replace("Views", "ViewModels").Replace("Page", "ViewModel");
            
            // Tìm và chọn mục điều hướng đúng
            var items = MenuItems?.OfType<NavigationViewItem>();
            if (items != null)
            {
                foreach (var item in items)
                {
                    // Kiểm tra mục menu chính
                    if (item.MenuItems.Count > 0)
                    {
                        foreach (var subItem in item.MenuItems.OfType<NavigationViewItem>())
                        {
                            if (subItem.GetValue(NavigationHelper.NavigateToProperty)?.ToString() == viewModelTypeName)
                            {
                                Selected = subItem;
                                item.IsExpanded = true;
                                break;
                            }
                        }
                    }
                }
            }

            Debug.WriteLine($"Lưu trang cuối: {viewModelTypeName}");
            await _lastPageService.SaveLastPageAsync(viewModelTypeName);
        }
    }

    // Phương thức quay lại
    [RelayCommand]
    private void GoBack()
    {
        if (_navigationService.CanGoBack)
        {
            _navigationService.GoBack();
        }
    }

    // Phương thức khởi tạo
    public async Task InitializeAsync()
    {
        var lastPage = await _lastPageService.GetLastPageAsync();
        Debug.WriteLine($"Lấy trang cuối: {lastPage}");

        if (!string.IsNullOrEmpty(lastPage))
        {
            Debug.WriteLine($"Cố gắng điều hướng đến: {lastPage}");
            _navigationService.NavigateTo(lastPage);
        }
        else
        {
            Debug.WriteLine("Không tìm thấy trang cuối, chuyển đến trang mặc định");
            _navigationService.NavigateTo(typeof(InventoryReportViewModel).FullName!);
        }
    }

    public void UnregisterEvents()
    {
        _navigationService.Navigated -= OnNavigated;
    }

    // Phương thức tìm kiếm
    [RelayCommand]
    private async Task SearchAsync(string? query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return;

        SearchQuery = query;
        await _dialogService.ShowSearchDialogAsync(query);
    }

    // Xử lý khi thay đổi từ khóa tìm kiếm
    partial void OnSearchQueryChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            SearchQuery = string.Empty;
        }
    }
}